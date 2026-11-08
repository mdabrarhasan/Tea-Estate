import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  UrlTree,
} from '@angular/router';
import { PermissionService } from '../service/permission/permission.service';

@Injectable({ providedIn: 'root' })
export class PermissionGuard implements CanActivate {
  constructor(
    private perm: PermissionService,
    private router: Router,
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): boolean | UrlTree {
    const path = state.url.split('?')[0].split(';')[0]; // full path, params stripped
    if (this.perm.canViewPath(path)) return true;
    return this.router.parseUrl('/dashboard'); // blocked → dashboard
  }
}
