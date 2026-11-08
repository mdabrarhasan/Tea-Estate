import { Component, Input } from '@angular/core';
import { MenuService } from '../../service/menu/menu.service';
import { PermissionService } from '../../service/permission/permission.service';

declare var bootstrap: any;

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
})
export class MenuComponent {
  @Input() isCollapsed: boolean = false;

  menu: any;
  submenu: any;
  activeMenuId: number | null = null;

  enroll: string | null = '';
  unit: string | null = '';

  constructor(
    private service: MenuService,
    private perm: PermissionService,
  ) {}

  ngOnInit(): void {
    const userId = localStorage.getItem('Enroll');
    this.Menu(userId);
    this.SubMenu(userId);
    this.enroll = localStorage.getItem('token');
    this.unit = localStorage.getItem('Enroll');
    this.isCollapsed = localStorage.getItem('sidebarCollapsed') === 'true';
    window.addEventListener('collapseSidebar', () => {
      this.closeAllSubmenus();
    });
  }

  Menu(UserID: any) {
    this.service.Menu(UserID).subscribe((data: any[]) => {
      this.menu = data;
      console.log(data);
    });
  }

  SubMenu(UserID: any) {
    this.service.SubMenu(UserID).subscribe((data: any[]) => {
      this.submenu = data.filter((s) => this.perm.canViewPath(s.routerlink));
      console.log(data);
    });
  }

  toggleforid(id: string) {
    localStorage.setItem('selectedMenuId', id);
  }

  toggleMenu(menuId: number) {
    const sidebarCollapsed =
      localStorage.getItem('sidebarCollapsed') === 'true';
    if (sidebarCollapsed) {
      this.expandSidebar();
      return;
    }

    this.activeMenuId = this.activeMenuId === menuId ? null : menuId;
  }

  expandSidebar() {
    localStorage.setItem('sidebarCollapsed', 'false');
    window.dispatchEvent(new CustomEvent('expandSidebar'));
  }

  closeAllSubmenus() {
    this.activeMenuId = null;
  }

  toggleMobileMenu(event: any) {
    var sidebarsize =
      document.documentElement.getAttribute('data-sidebar-size');
    if (sidebarsize == 'sm-hover-active') {
      document.documentElement.setAttribute('data-sidebar-size', 'sm-hover');
    } else {
      document.documentElement.setAttribute(
        'data-sidebar-size',
        'sm-hover-active',
      );
    }
  }

  SidebarHide() {
    document.body.classList.remove('vertical-sidebar-enable');
  }
}
