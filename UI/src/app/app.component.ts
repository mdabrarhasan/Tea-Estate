import {
  Component,
  ViewEncapsulation,
  HostListener,
  ElementRef,
  OnInit,
  OnDestroy,
} from '@angular/core';
import { LoginService } from './service/login/login.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import {
  NotificationService,
  NotificationSummary,
} from './service/notification/notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'TEALINK.UI';
  username: any;
  designation: any;
  isNotiOpen = false;
  isMsgOpen = false;
  notiLoading = false;
  notiSummary: NotificationSummary | null = null;
  isSidebarCollapsed = false;
  isMobileView = false;
  showMobileMenu = false;
  isSearchOpen = false;
  searchTerm = '';
  deviceType: 'mobile' | 'tablet' | 'laptop' | 'desktop' | 'large' = 'desktop';

  private touchStartX = 0;
  private touchEndX = 0;
  private readonly SWIPE_THRESHOLD = 50;
  private loginSubscription?: Subscription;

  constructor(
    public loginservice: LoginService,
    private router: Router,
    private notiService: NotificationService,
    private eref: ElementRef,
  ) {}

  onSearch(): void {
    const term = this.searchTerm.trim();
    if (!term) {
      return;
    }

    console.log('Searching:', term);
  }

  toggleNoti(): void {
    this.isNotiOpen = !this.isNotiOpen;
    this.isMsgOpen = false;
  }

  toggleMsg(): void {
    this.isMsgOpen = !this.isMsgOpen;
    this.isNotiOpen = false;
  }

  loadNotiSummary(): void {
    const unit = localStorage.getItem('LoginUnit');
    if (!unit) return;
    this.notiLoading = true;
    this.notiService.getSummary().subscribe({
      next: (res) => {
        this.notiSummary = res && res.length ? res[0] : null;
        this.notiLoading = false;
      },
      error: () => {
        this.notiLoading = false;
      },
    });
  }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.loginservice.submitted = true;
      this.loadUserData();
      this.loadNotiSummary();
    } else {
      this.loginservice.submitted = false;
      this.router.navigate(['/login']);
    }

    this.checkScreenSize();

    const savedSidebarState = localStorage.getItem('sidebarCollapsed');
    if (savedSidebarState !== null) {
      this.isSidebarCollapsed = savedSidebarState === 'true';
    }

    window.addEventListener('expandSidebar', () => {
      this.isSidebarCollapsed = false;
    });

    if (!this.isMobileView) {
      this.showMobileMenu = false;
      document.body.style.overflow = '';
    }
    this.loginSubscription = this.loginservice.userLoggedIn$.subscribe(() => {
      this.loadUserData();
      this.loginservice.submitted = true;
      this.loadNotiSummary();
    });
  }

  private loadUserData() {
    this.username = localStorage.getItem('Name');
    this.designation = localStorage.getItem('Designation');
  }

  ngOnDestroy() {
    if (this.loginSubscription) {
      this.loginSubscription.unsubscribe();
    }
  }

  get isMobileSidebarOpen(): boolean {
    return this.showMobileMenu;
  }

  @HostListener('document:click', ['$event'])
  onDocClick(event: MouseEvent): void {
    if (!this.eref.nativeElement.contains(event.target)) {
      this.isNotiOpen = false;
      this.isMsgOpen = false;
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event?: any) {
    this.checkScreenSize();

    if (window.innerWidth > 992 && this.showMobileMenu) {
      this.showMobileMenu = false;
      this._applyMobileDOMState();
    }
  }

  checkScreenSize() {
    const width = window.innerWidth;

    if (width < 576) {
      this.deviceType = 'mobile';
      this.isMobileView = true;
    } else if (width >= 576 && width < 992) {
      this.deviceType = 'tablet';
      this.isMobileView = true;
    } else if (width >= 992 && width < 1200) {
      this.deviceType = 'laptop';
      this.isMobileView = false;
    } else if (width >= 1200 && width < 1400) {
      this.deviceType = 'desktop';
      this.isMobileView = false;
    } else {
      this.deviceType = 'large';
      this.isMobileView = false;
    }

    if (!this.isMobileView) {
      this.showMobileMenu = false;
      document.body.style.overflow = '';
    }

    console.log('Device Type:', this.deviceType, '| Width:', width);
  }

  @HostListener('touchstart', ['$event'])
  handleTouchStart(event: TouchEvent) {
    if (this.isMobileView) {
      this.touchStartX = event.changedTouches[0].screenX;
    }
  }

  @HostListener('touchend', ['$event'])
  handleTouchEnd(event: TouchEvent) {
    if (this.isMobileView) {
      this.touchEndX = event.changedTouches[0].screenX;
      this.handleSwipe();
    }
  }

  private handleSwipe() {
    const swipeDistance = this.touchEndX - this.touchStartX;

    if (swipeDistance > this.SWIPE_THRESHOLD && this.touchStartX < 50) {
      if (!this.showMobileMenu) {
        this.showMobileMenu = true;
        document.body.style.overflow = 'hidden';
        this._applyMobileDOMState();
        console.log('Sidebar opened via swipe');
      }
    }

    if (swipeDistance < -this.SWIPE_THRESHOLD && this.showMobileMenu) {
      this.closeMobileMenu();
      console.log('Sidebar closed via swipe');
    }
  }

  toggleSidebar() {
    if (this.isMobileView) {
      this.showMobileMenu = !this.showMobileMenu;
      document.body.style.overflow = this.showMobileMenu ? 'hidden' : '';
      this._applyMobileDOMState();
    } else {
      this.isSidebarCollapsed = !this.isSidebarCollapsed;
      localStorage.setItem(
        'sidebarCollapsed',
        this.isSidebarCollapsed.toString(),
      );

      if (this.isSidebarCollapsed) {
        window.dispatchEvent(new CustomEvent('collapseSidebar'));
      } else {
        window.dispatchEvent(new CustomEvent('expandSidebar'));
      }
    }

    console.log(
      'Sidebar collapsed:',
      this.isSidebarCollapsed,
      '| Mobile open:',
      this.showMobileMenu,
    );
  }

  closeMobileMenu() {
    if (this.isMobileView) {
      this.showMobileMenu = false;
      document.body.style.overflow = '';
      this._applyMobileDOMState();
    }
  }

  closeMobileSidebar() {
    this.closeMobileMenu();
  }

  private _applyMobileDOMState() {
    const sidebarEl = document.querySelector('.app-menu.navbar-menu');
    const overlayEl = document.querySelector('.mobile-overlay');

    if (this.showMobileMenu) {
      sidebarEl?.classList.add('mobile-open');
      overlayEl?.classList.add('show');
      document.body.classList.add('no-scroll-when-sidebar-open');
    } else {
      sidebarEl?.classList.remove('mobile-open');
      overlayEl?.classList.remove('show');
      document.body.classList.remove('no-scroll-when-sidebar-open');
    }
  }

  @HostListener('window:keydown', ['$event'])
  handleKeyDown(event: KeyboardEvent) {
    if (event.key === 'Escape' && this.showMobileMenu) {
      this.closeMobileMenu();
    }
  }

  Logout() {
    localStorage.clear();
    localStorage.setItem('token', '');
    this.loginservice.submitted = false;
    this.username = null;
    this.designation = null;
    this.router.navigate(['/login']);
  }
}
