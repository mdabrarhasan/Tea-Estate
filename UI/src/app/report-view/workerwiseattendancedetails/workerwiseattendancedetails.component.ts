import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../service/common/common.service';

/**
 * Worker Wise Attendance Details — SSRS-only report page.
 *
 * Same three-zone layout as Daily Attendance Details: date range on top, a
 * left rail of filter categories, and a center panel that loads the selected
 * category's items as checkboxes. Nothing is fetched to a grid; "Generate"
 * opens the SSRS report in a popup with all checked ids passed as
 * comma-joined, single-value parameters (SP side uses STRING_SPLIT;
 * empty string = all).
 *
 * Differs from Daily Attendance Details: no Section, no Attendance Type;
 * Shift (TBLEMPLOYEETEAM.TEAMID, used as SHIFTID on the fact tables) instead.
 */

// ---- Filter category keys (internal identifiers, not SSRS params) ----
type FilterKey =
  | 'BOOK'
  | 'WORKERTYPE'
  | 'EMPLOYEE'
  | 'DIVISION'
  | 'KAMJARI'
  | 'SHIFT';

interface FilterItem {
  id: any;
  name: string;
}

interface FilterCategory {
  key: FilterKey;
  label: string; // shown on the left rail button
  icon: string; // boxicons class
  ssrsParam: string; // EXACT .rdl parameter name — change here if your report differs
  items: FilterItem[];
  loaded: boolean;
  loading: boolean;
  selected: Set<any>; // checked ids for this category
}

@Component({
  selector: 'app-worker-wise-attendance-details',
  templateUrl: './workerwiseattendancedetails.component.html',
  styleUrl: './workerwiseattendancedetails.component.css',
})
export class WorkerWiseAttendanceDetailsComponent {
  // ---- SSRS endpoint config (matches your existing report pages) ----
  private readonly SSRS_BASE = 'Dummy_String';
  private readonly SSRS_FOLDER = '/TeaLink/WorkerWiseAttendanceDetails';
  private readonly SSRS_TAIL = '&rs:Embed=true&rc:LinkTarget=_self';

  submitted = false;

  DateForm: FormGroup = new FormGroup({
    FROMDATE: new FormControl(''),
    TODATE: new FormControl(''),
  });

  // ---- Left-rail categories. ssrsParam values are the ONLY things to touch
  //      if your .rdl parameter names differ. ----
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
      key: 'WORKERTYPE',
      label: 'Worker Type',
      icon: 'bx-category',
      ssrsParam: 'WORKERTYPEID',
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
    {
      key: 'KAMJARI',
      label: 'Kamjari',
      icon: 'bx-briefcase-alt-2',
      ssrsParam: 'KAMJARIID',
      items: [],
      loaded: false,
      loading: false,
      selected: new Set(),
    },
    {
      key: 'SHIFT',
      label: 'Shift',
      icon: 'bx-time-five',
      ssrsParam: 'SHIFTID',
      items: [],
      loaded: false,
      loading: false,
      selected: new Set(),
    },
  ];

  activeKey: FilterKey | null = null;

  // search box for the center panel (helps with long employee lists)
  filterText = '';

  constructor(
    private cs: CommonService,
    private toastr: ToastrService,
  ) {}

  get active(): FilterCategory | null {
    return this.categories.find((c) => c.key === this.activeKey) ?? null;
  }

  // Items shown in the center panel, filtered by the search box.
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
        this.cs.GetBook().subscribe(done, fail);
        break;
      case 'WORKERTYPE':
        this.cs.GetWorkerType().subscribe(done, fail);
        break;
      case 'EMPLOYEE':
        this.cs.GetEmployee().subscribe(done, fail);
        break;
      case 'DIVISION':
        this.cs.GetDivision().subscribe(done, fail);
        break;
      case 'KAMJARI':
        this.cs.GetKamjari().subscribe(done, fail);
        break;
      case 'SHIFT':
        // If SPGETSHIT already returns every active shift for the unit
        // (ISOT = 0 and ISOT = 1), swap this for this.cs.GetShit().
        this.cs.GetShiftForReport().subscribe(done, fail);
        break;
    }
  }

  // ---- checkbox helpers ----
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

  // ---- top-bar actions ----
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

    // Build one comma-joined value per category. Empty string => "all" (SP side).
    let paramString = '';
    this.categories.forEach((c) => {
      let joined = '';
      const total = c.items.length;
      const chosen = c.selected.size;

      // Only collapse to "all" when the list was actually loaded (total > 0)
      // and every loaded item is selected. Otherwise send the chosen ids.
      // This keeps the URL short when thousands of employees are ticked.
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
