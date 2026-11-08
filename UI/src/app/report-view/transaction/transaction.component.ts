import {
  Component,
  OnInit,
  AfterViewInit,
  OnDestroy,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { CommonService } from '../../service/common/common.service';
import { FormBuilder, FormGroup } from '@angular/forms';

interface ReportList {
  id: number;
  name: string;
  reporturl: string;
  mastertypeid: number;
}

interface CategorytList {
  id: number;
  name: string;
}

interface ReportFilter {
  id: number;
  name: string;
  filtercode: string;
  icon: string;
  enabled: boolean;
}

interface SelectedFilterData {
  filterId: number;

  filterName: string; //FIRST LETTER OF FILTERNAME: L

  filtercode: string;

  fromDate: string; //FDATE

  toDate: string; //TDATE

  data: {
    id: number; //LID

    name: string; //LNAME
  }[];

  selectedItems: {
    id: number; //LID

    name: string; //LNAME
  }[];
}

@Component({
  selector: 'app-transaction',

  templateUrl: './transaction.component.html',

  styleUrls: ['./transaction.component.css'],
})
export class transactionComponent implements OnInit, AfterViewInit, OnDestroy {
  reportList: ReportList[] = [];

  categoryList: CategorytList[] = [];

  filterList: ReportFilter[] = [];

  selectedReportId: number = 0;

  selectedReport: ReportList | null = null;

  filterData: any[] = [];

  selectedFilters: ReportFilter[] = [];

  currentPage: number = 1;

  pageSize: number = 1;

  totalPages: number = 1;

  pagedSelectedFilters: ReportFilter[] = [];

  selectedFilterData: SelectedFilterData[] = [];

  loading = false;

  form!: FormGroup;

  @ViewChild('workspace', { static: false })
  workspace!: ElementRef<HTMLDivElement>;

  private resizeObserver?: ResizeObserver;

  private minWidth = 450;
  private minHeight = 350;

  private maxWidth = 1600;
  private maxHeight = 1200;

  hasEnabledFilters = false;

  constructor(
    private commonService: CommonService,
    private fb: FormBuilder,
  ) {}

  ngOnInit(): void {
    this.loadCategoryList();

    this.loadFilters();

    const currentYear = new Date().getFullYear();

    const fromDate = new Date(
      currentYear,
      0, // January (month index starts from 0)
      1,
    );

    const toDate = new Date(currentYear + 1, 0, 1);

    this.form = this.fb.group({
      CATEGORYID: [''],

      REPORTTYPE: [''],

      FROMDATE: [this.formatDate(fromDate)],

      TODATE: [this.formatDate(toDate)],
    });
  }

  //==========================================
  // LOAD CATEGORY DROPDOWN LIST TRANSACTION
  //==========================================
  loadCategoryList(): void {
    this.commonService.GetCategoryListTransaction().subscribe({
      next: (response: any[]) => {
        this.categoryList = response;
      },
      error: (error) => {
        console.error('Error loading category list:', error);
      },
    });
  }

  //==========================================
  // LOAD REPORT DROPDOWN LIST TRANSACTION
  //==========================================

  loadReportList(categoryId: string): void {
    this.clearReport();
    // Clear report list
    this.reportList = null as any;
    //Clear selected report id
    this.selectedReportId = 0;

    this.form.patchValue({
      REPORTTYPE: '',
    });

    const id = Number(categoryId);

    if (id <= 0) {
      this.reportList = [];

      return;
    }

    this.commonService
      .GetReportListTransaction(id)

      .subscribe({
        next: (response: ReportList[]) => {
          this.reportList = response;
        },

        error: (error) => {
          console.error(
            'Error loading report list:',

            error,
          );
        },
      });
  }

  //==========================================
  // LOAD FILTER BUTTONS
  //==========================================

  loadFilters(): void {
    this.commonService
      .GetReportFilters()

      .subscribe({
        next: (response: any[]) => {
          this.filterList = response.map((item) => ({
            id: Number(item.id),

            name: item.name,

            filtercode: item.filtercode,

            icon: this.getFilterIcon(item.name),

            enabled: false,
          }));
        },

        error: (error) => {
          console.error('Error loading filter list:', error);
        },
      });
  }

  //==========================================
  // REPORT CHANGE EVENT
  //==========================================

  reportChanged(reportId: string): void {
    this.clearReport();

    this.selectedReportId = Number(reportId);

    // Store selected report
    this.selectedReport =
      this.reportList.find((r) => r.id === this.selectedReportId) ?? null;

    // Reset enabled filter status
    this.hasEnabledFilters = false;

    // No report selected
    if (!this.selectedReportId || !this.selectedReport) {
      this.disableAllFilters();

      return;
    }

    // Load filters assigned to selected report
    this.commonService
      .GetReportAssignedFilters(this.selectedReportId)
      .subscribe({
        next: (response: any[]) => {
          const allowedFilterIds = response.map((x) => Number(x.id));

          // Enable/disable filters
          this.filterList.forEach((filter) => {
            filter.enabled = allowedFilterIds.includes(filter.id);
          });

          // ======================================
          // CHECK WHETHER CURRENT REPORT
          // HAS ANY ENABLED FILTER
          // ======================================

          this.hasEnabledFilters = this.filterList.some(
            (filter) => filter.enabled,
          );

          console.log('Report:', this.selectedReport?.name);

          console.log('Has Enabled Filters:', this.hasEnabledFilters);
        },

        error: (error) => {
          console.error('Error loading assigned filters:', error);

          this.disableAllFilters();

          this.hasEnabledFilters = false;
        },
      });
  }

  //==========================================
  // DISABLE ALL FILTER BUTTONS
  //==========================================

  disableAllFilters(): void {
    this.filterList.forEach((filter) => {
      filter.enabled = false;
    });
  }

  //==========================================
  // FILTER ICON MAPPING
  //==========================================

  getFilterIcon(filterName: string): string {
    const iconMap: { [key: string]: string } = {
      Book: 'bx bx-book',

      Category: 'bx bx-category',

      Employee: 'bx bx-user',

      Division: 'bx bx-buildings',

      Section: 'bx bx-sitemap',

      Operation: 'bx bx-cog',

      Attendance: 'bx bx-calendar-check',
    };

    return iconMap[filterName] || 'bx bx-filter-alt';
  }

  //==========================================
  // LEFT FILTER BUTTON CLICK
  //==========================================

  openFilter(filter: ReportFilter): void {
    const index = this.selectedFilters.findIndex((x) => x.id === filter.id);

    // UNCHECK LEFT FILTER
    if (index >= 0) {
      // Remove from selected filters
      this.selectedFilters.splice(index, 1);

      // Remove stored filter data and selected workspace items
      this.selectedFilterData = this.selectedFilterData.filter(
        (x) => x.filterId !== filter.id,
      );

      // Refresh pagination
      this.updatePagination();

      return;
    }

    // CHECK LEFT FILTER
    this.selectedFilters.push(filter);

    // Create storage object
    const filterStorage: SelectedFilterData = {
      filterId: filter.id,

      filterName: filter.name,

      filtercode: filter.filtercode,

      fromDate: this.form.get('FROMDATE')?.value,

      toDate: this.form.get('TODATE')?.value,

      data: [],

      selectedItems: [],
    };

    this.selectedFilterData.push(filterStorage);

    // Load workspace data only once
    this.commonService.GetLeftFilterData(filter.id).subscribe({
      next: (data) => {
        filterStorage.data = data;

        // Show current filter data
        this.filterData = data;

        this.updatePagination();
      },

      error: (err) => {
        console.error(err);

        filterStorage.data = [];

        this.filterData = [];

        this.updatePagination();
      },
    });
  }

  //==========================================
  // WORKSPACE SELECTION
  //==========================================
  isWorkspaceItemSelected(itemId: number): boolean {
    if (this.pagedSelectedFilters.length === 0) {
      return false;
    }

    const currentFilter = this.pagedSelectedFilters[0];

    const storage = this.selectedFilterData.find(
      (x) => x.filterId === currentFilter.id,
    );

    if (!storage) {
      return false;
    }

    return storage.selectedItems.some((x) => x.id === itemId);
  }

  selectWorkspaceItem(item: any): void {
    if (this.pagedSelectedFilters.length === 0) {
      return;
    }

    const currentFilter = this.pagedSelectedFilters[0];

    const storage = this.selectedFilterData.find(
      (x) => x.filterId === currentFilter.id,
    );

    if (!storage) {
      return;
    }

    const index = storage.selectedItems.findIndex((x) => x.id === item.id);

    if (index >= 0) {
      // remove unchecked item
      storage.selectedItems.splice(index, 1);
    } else {
      // add checked item
      storage.selectedItems.push({
        id: item.id,

        name: item.name,
      });
    }
  }

  //==========================================
  // GENERATE REPORT
  //==========================================

  generateReport(): void {
    // Report must be selected
    if (!this.selectedReport) {
      alert('Please select from Report List!');
      return;
    }

    // At least one filter must be selected
    else if (this.hasEnabledFilters && this.selectedFilters.length === 0) {
      alert('Please select at least one filter!');
      return;
    } else {
      const motherUrl = 'Dummy_String';

      const folderUrl = '/TeaLink';

      const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

      const params = new URLSearchParams();

      // COMMON PARAMETERS---------------------------

      // UNITID
      const unitId = Number(localStorage.getItem('LoginUnit'));

      params.append('UNITID', unitId.toString());

      // FROM DATE
      const fromDate = this.form.get('FROMDATE')?.value;

      if (fromDate) {
        params.append('FDATE', fromDate);
      }

      // TO DATE
      const toDate = this.form.get('TODATE')?.value;

      if (toDate) {
        params.append('TDATE', toDate);
      }

      // FILTER PARAMETERS-------------------

      this.selectedFilterData.forEach((filter) => {
        if (!filter.filterName) {
          return;
        } else {
          // Employee -> E
          // Division -> D
          // Section -> S
          const prefix = filter.filtercode.trim().toUpperCase();

          // If nothing selected, send NULL
          if (filter.selectedItems.length === 0) {
            params.append(`${prefix}ID`, '0');

            params.append(`${prefix}NAME`, '0');

            return;
          }

          // Selected items only
          const ids = filter.selectedItems.map((x) => x.id).join(',');

          const names = filter.selectedItems.map((x) => x.name).join(',');

          params.append(`${prefix}ID`, ids);

          params.append(`${prefix}NAME`, names);
        }
      });

      // BUILD REPORT URL----------------------

      const fullUrl = `${motherUrl}${encodeURIComponent(
        folderUrl + this.selectedReport.reporturl,
      )}&${params.toString()}${otherUrl}`;

      //console.log(fullUrl);

      // OPEN REPORT-------------------

      const reportWindow = window.open(
        fullUrl,
        '_blank',
        'scrollbars=yes,toolbar=0,height=700,width=1200,top=70,left=50',
      );

      if (reportWindow) {
        reportWindow.focus();
      } else {
        alert('Report generation failed.');
      }
    }
  }

  //==========================================
  // CLEAR REPORT
  //==========================================
  clearReport(): void {
    // Clear selected filter checklist
    this.selectedFilters = [];

    this.selectedFilterData = [];

    // Clear pagination
    this.currentPage = 1;

    this.totalPages = 1;

    this.pagedSelectedFilters = [];

    // Clear workspace data
    this.filterData = [];

    // Clear report selection if required
    //this.selectedReportId = 0;
  }

  //-----------------------------------------------------------------
  ngAfterViewInit(): void {
    const element = this.workspace.nativeElement;

    this.resizeObserver = new ResizeObserver(() => {
      let width = element.offsetWidth;
      let height = element.offsetHeight;

      if (width < this.minWidth) {
        element.style.width = this.minWidth + 'px';
      }

      if (height < this.minHeight) {
        element.style.height = this.minHeight + 'px';
      }

      if (width > this.maxWidth) {
        element.style.width = this.maxWidth + 'px';
      }

      if (height > this.maxHeight) {
        element.style.height = this.maxHeight + 'px';
      }
    });

    this.resizeObserver.observe(element);
  }

  ngOnDestroy(): void {
    if (this.resizeObserver) {
      this.resizeObserver.disconnect();
    }
  }

  private formatDate(date: Date): string {
    const year = date.getFullYear();

    const month = String(date.getMonth() + 1).padStart(2, '0');

    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  private updatePagination(): void {
    this.totalPages = this.selectedFilters.length;

    if (this.totalPages === 0) {
      this.totalPages = 1;
    }

    if (this.currentPage > this.totalPages) {
      this.currentPage = this.totalPages;
    }

    this.loadPageData();
  }

  private loadPageData(): void {
    const index = this.currentPage - 1;

    this.pagedSelectedFilters = this.selectedFilters.slice(
      index,
      index + this.pageSize,
    );

    if (this.pagedSelectedFilters.length > 0) {
      const currentFilter = this.pagedSelectedFilters[0];

      const storedFilter = this.selectedFilterData.find(
        (x) => x.filterId === currentFilter.id,
      );

      if (storedFilter) {
        this.filterData = storedFilter.data;
      } else {
        this.filterData = [];
      }
    } else {
      this.filterData = [];
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;

      this.loadPageData();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;

      this.loadPageData();
    }
  }
}
