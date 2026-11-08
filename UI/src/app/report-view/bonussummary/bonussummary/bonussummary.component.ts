import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../../service/common/common.service';

/**
 * Bonus Summary — SSRS-only report page.
 *
 * PERMANENT workers only. Three filters: Book, Employee, Division.
 * Book and Employee are scoped to permanent workers server-side
 * (SPGETBOOKPERMANENT / SPGETEMPLOYEEPERMANENT). Division is unchanged
 * because a division contains all worker types.
 *
 * "Generate" opens the SSRS report in a popup; checked ids are passed as
 * comma-joined multi-value params (SP uses STRING_SPLIT; empty = all).
 */

type FilterKey = 'BOOK' | 'EMPLOYEE' | 'DIVISION';

interface FilterItem {
  id: any;
  name: string;
}

interface FilterCategory {
  key: FilterKey;
  label: string;
  icon: string;
  ssrsParam: string; // EXACT .rdl parameter name
  items: FilterItem[];
  loaded: boolean;
  loading: boolean;
  selected: Set<any>;
}

@Component({
  selector: 'app-bonus-summary',
  templateUrl: './bonussummary.component.html',
  styleUrl: './bonussummary.component.css',
})
export class BonusSummaryComponent implements OnInit {
  private readonly SSRS_BASE = 'Dummy_String';
  private readonly SSRS_FOLDER = '/TeaLink/BonusSummary';
  private readonly SSRS_TAIL = '&rs:Embed=true&rc:LinkTarget=_self';

  submitted = false;

  DateForm: FormGroup = new FormGroup({
    FROMDATE: new FormControl(''),
    TODATE: new FormControl(''),
  });

  categories: FilterCategory[] = [
    {
      key: 'BOOK',
      label: 'Book',
      icon: 'bx-book',
      ssrsParam: 'BOOKID',
      items: [],
      loaded: false,
      loading: false,
      selected: new Set(),
    },
    {
      key: 'EMPLOYEE',
      label: 'Employee',
      icon: 'bx-user',
      ssrsParam: 'CODE',
      items: [],
      loaded: false,
      loading: false,
      selected: new Set(),
    },
    {
      key: 'DIVISION',
      label: 'Division',
      icon: 'bx-grid-alt',
      ssrsParam: 'DIVISIONID',
      items: [],
      loaded: false,
      loading: false,
      selected: new Set(),
    },
  ];

  activeKey: FilterKey | null = null;
  filterText = '';

  constructor(
    private cs: CommonService,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {}

  get active(): FilterCategory | null {
    return this.categories.find((c) => c.key === this.activeKey) ?? null;
  }

  get visibleItems(): FilterItem[] {
    if (!this.active) return [];
    const t = this.filterText.trim().toLowerCase();
    if (!t) return this.active.items;
    return this.active.items.filter((i) =>
      (i.name ?? '').toLowerCase().includes(t),
    );
  }

  selectCategory(key: FilterKey): void {
    this.activeKey = key;
    this.filterText = '';
    const cat = this.active;
    if (cat && !cat.loaded && !cat.loading) {
      this.loadCategory(cat);
    }
  }

  private loadCategory(cat: FilterCategory): void {
    cat.loading = true;

    const done = (data: any[]) => {
      cat.items = (data || []).map((d) => ({
        id: d.id ?? d.Id,
        name: d.name ?? d.Name,
      }));
      cat.loaded = true;
      cat.loading = false;
    };
    const fail = () => {
      cat.loading = false;
      this.toastr.error('Could not load ' + cat.label + '.', 'close');
    };

    switch (cat.key) {
      case 'BOOK':
        // permanent-only books
        this.cs.GetBookPermanent().subscribe(done, fail);
        break;
      case 'EMPLOYEE':
        // permanent-only employees
        this.cs.GetEmployeePermanent().subscribe(done, fail);
        break;
      case 'DIVISION':
        // unchanged — divisions hold all worker types
        this.cs.GetDivision().subscribe(done, fail);
        break;
    }
  }

  isChecked(id: any): boolean {
    return this.active?.selected.has(id) ?? false;
  }

  toggle(id: any): void {
    if (!this.active) return;
    const set = this.active.selected;
    if (set.has(id)) set.delete(id);
    else set.add(id);
  }

  allVisibleChecked(): boolean {
    if (!this.active) return false;
    const vis = this.visibleItems;
    return vis.length > 0 && vis.every((i) => this.active!.selected.has(i.id));
  }

  toggleAllVisible(): void {
    if (!this.active) return;
    const vis = this.visibleItems;
    if (this.allVisibleChecked()) {
      vis.forEach((i) => this.active!.selected.delete(i.id));
    } else {
      vis.forEach((i) => this.active!.selected.add(i.id));
    }
  }

  selectedCount(key: FilterKey): number {
    return this.categories.find((c) => c.key === key)!.selected.size;
  }

  clearAll(): void {
    this.categories.forEach((c) => c.selected.clear());
    this.DateForm.reset({ FROMDATE: '', TODATE: '' });
    this.filterText = '';
    this.activeKey = null;
    this.submitted = false;
    this.toastr.info('Selections cleared.', 'close');
  }

  generate(): void {
    const from = this.DateForm.value.FROMDATE;
    const to = this.DateForm.value.TODATE;

    if (!from || !to) {
      this.toastr.error('Select both From and To dates.', 'close');
      return;
    }
    if (from > to) {
      this.toastr.error('From date cannot be after To date.', 'close');
      return;
    }

    const unit = localStorage.getItem('LoginUnit');

    let paramString = '';
    this.categories.forEach((c) => {
      let joined = '';
      const total = c.items.length;
      const chosen = c.selected.size;

      // collapse to "all" only when everything loaded is selected
      if (chosen > 0 && !(total > 0 && chosen === total)) {
        joined = Array.from(c.selected).join(',');
      }
      paramString += '&' + c.ssrsParam + '=' + encodeURIComponent(joined);
    });

    const url =
      this.SSRS_BASE +
      encodeURIComponent(this.SSRS_FOLDER) +
      '&FROMDATE=' +
      encodeURIComponent(from) +
      '&TODATE=' +
      encodeURIComponent(to) +
      '&UNITID=' +
      encodeURIComponent(unit ?? '') +
      paramString +
      this.SSRS_TAIL;

    const win = window.open(
      url,
      '_blank',
      'scrollbars=yes,toolbar=0,height=600,width=1000,top=70,left=50',
    );
    if (win && win.focus) win.focus();
  }
}
