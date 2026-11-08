// component/operatorpermission/operatorpermission.component.ts
import { Component, OnInit } from '@angular/core';
import { OperatorPermissionService } from '../../service/operatorpermission/operatorpermission.service';
import { ToastrService } from 'ngx-toastr';

interface PermRow {
  menuid: number;
  menuname: string;
  menuseq: number;
  submenuid: number;
  submenuname: string;
  routerlink: string;
  submenuseq: number;
  canview: boolean;
  caninsert: boolean;
  canupdate: boolean;
  candelete: boolean;
  canprint: boolean;
}
interface MenuGroup {
  menuid: number;
  menuname: string;
  rows: PermRow[];
}
type PermAction =
  | 'canview'
  | 'caninsert'
  | 'canupdate'
  | 'candelete'
  | 'canprint';

@Component({
  selector: 'app-operatorpermission',
  templateUrl: './operatorpermission.component.html',
  styleUrl: './operatorpermission.component.css',
})
export class OperatorpermissionComponent implements OnInit {
  groups: MenuGroup[] = [];
  loading = false;
  savingKeys = new Set<string>();

  actions: { key: PermAction; label: string }[] = [
    { key: 'canview', label: 'View' },
    { key: 'caninsert', label: 'Insert' },
    { key: 'canupdate', label: 'Update' },
    { key: 'candelete', label: 'Delete' },
    { key: 'canprint', label: 'Print' },
  ];

  constructor(
    private service: OperatorPermissionService,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.service.GetPermissions().subscribe({
      next: (data: PermRow[]) => {
        this.groups = this.groupByMenu(data);
        this.loading = false;
      },
      error: () => {
        this.toastr.error('Failed to load permissions', 'Error');
        this.loading = false;
      },
    });
  }

  private groupByMenu(rows: PermRow[]): MenuGroup[] {
    const map = new Map<number, MenuGroup>();
    for (const r of rows) {
      if (!map.has(r.menuid))
        map.set(r.menuid, { menuid: r.menuid, menuname: r.menuname, rows: [] });
      map.get(r.menuid)!.rows.push(r);
    }
    return Array.from(map.values());
  }

  toggle(row: PermRow, action: PermAction): void {
    const key = `${row.submenuid}:${action}`;
    if (this.savingKeys.has(key)) return;

    const prev = row[action];
    row[action] = !prev;
    this.savingKeys.add(key);

    const dto = {
      menuId: row.menuid,
      subMenuId: row.submenuid,
      canView: row.canview,
      canInsert: row.caninsert,
      canUpdate: row.canupdate,
      canDelete: row.candelete,
      canPrint: row.canprint,
      updatedBy: parseInt(localStorage.getItem('LoginEnroll') || '0'),
    };

    this.service.SavePermission(dto).subscribe({
      next: (res: any) => {
        this.savingKeys.delete(key);
        if (!res || !res.item2 || !res.item2.includes('success')) {
          row[action] = prev;
          this.toastr.error('Save failed', 'Error');
        }
      },
      error: () => {
        row[action] = prev;
        this.savingKeys.delete(key);
        this.toastr.error('Save failed', 'Error');
      },
    });
  }

  isSaving(row: PermRow, action: PermAction): boolean {
    return this.savingKeys.has(`${row.submenuid}:${action}`);
  }
}
