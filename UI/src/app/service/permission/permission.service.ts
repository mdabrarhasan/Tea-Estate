import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class PermissionService {
  private perms: any[] = [];

  constructor(private router: Router) {}

  private load(): void {
    if (this.perms.length === 0) {
      const raw = localStorage.getItem('permissions');
      this.perms = raw ? JSON.parse(raw) : [];
    }
  }

  refresh(): void {
    const raw = localStorage.getItem('permissions');
    this.perms = raw ? JSON.parse(raw) : [];
  }

  private isAdmin(): boolean {
    return localStorage.getItem('TypeId') === '2';
  }

  canHere(action: 'view' | 'insert' | 'update' | 'delete' | 'print'): boolean {
    if (this.isAdmin()) return true;
    this.load();
    const url = this.router.url.split('?')[0].split(';')[0]; // strip query + matrix params
    const row = this.perms.find((p) => p.routerlink === url);
    if (!row) return false;
    switch (action) {
      case 'view':
        return !!row.canview;
      case 'insert':
        return !!row.caninsert;
      case 'update':
        return !!row.canupdate;
      case 'delete':
        return !!row.candelete;
      case 'print':
        return !!row.canprint;
    }
  }

  can(
    subMenuId: number,
    action: 'view' | 'insert' | 'update' | 'delete' | 'print',
  ): boolean {
    if (this.isAdmin()) return true; // admins can do everything
    this.load();
    const row = this.perms.find((p) => p.submenuid === subMenuId);
    if (!row) return false; // no row => denied
    switch (action) {
      case 'view':
        return !!row.canview;
      case 'insert':
        return !!row.caninsert;
      case 'update':
        return !!row.canupdate;
      case 'delete':
        return !!row.candelete;
      case 'print':
        return !!row.canprint;
    }
  }

  canViewPath(path: string): boolean {
    if (this.isAdmin()) return true;
    this.load();
    const row = this.perms.find((p) => p.routerlink === path);
    return row ? !!row.canview : false;
  }
}
