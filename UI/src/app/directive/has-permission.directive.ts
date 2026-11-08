import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { PermissionService } from '../service/permission/permission.service';

@Directive({ selector: '[hasPermission]' })
export class HasPermissionDirective {
  constructor(
    private tpl: TemplateRef<any>,
    private vcr: ViewContainerRef,
    private perm: PermissionService,
  ) {}

  @Input() set hasPermission(
    action: 'view' | 'insert' | 'update' | 'delete' | 'print',
  ) {
    this.vcr.clear();
    if (this.perm.canHere(action)) {
      this.vcr.createEmbeddedView(this.tpl);
    }
  }
}
