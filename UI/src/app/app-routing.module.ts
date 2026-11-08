import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { CompanymanagementComponent } from './companymanagement/companymanagement/companymanagement.component';
import { UsermanagementComponent } from './usermanagement/usermanagement/usermanagement.component';
import { GardenmanagementComponent } from './gardenmanagement/gardenmanagement/gardenmanagement.component';
import { KamjarimanagementComponent } from './kamjarimanagement/kamjarimanagement/kamjarimanagement.component';
import { ShiftmanagementComponent } from './shiftmanagement/shiftmanagement/shiftmanagement.component';
import { WorkermanagementComponent } from './workermanagement/workermanagement/workermanagement.component';
import { HolidayComponent } from './holiday/holiday/holiday.component';
import { SectionmanagementComponent } from './sectionmanagement/sectionmanagement/sectionmanagement.component';
import { PluckingtypemanagementComponent } from './pluckingtypemanagement/pluckingtypemanagement/pluckingtypemanagement.component';
import { PrunetypemanagementComponent } from './prunetypemanagement/prunetypemanagement/prunetypemanagement.component';
import { ConfigurationComponent } from './configuration/configuration/configuration.component';
import { WagecalenderComponent } from './wagecalender/wagecalender/wagecalender.component';
import { RationmanagementComponent } from './rationmanagement/rationmanagement/rationmanagement.component';
import { DailypluckingComponent } from './dailyplucking/dailyplucking/dailyplucking.component';
import { NonpluckingactivitiesComponent } from './nonpluckingactivities/nonpluckingactivities/nonpluckingactivities.component';
import { LeafcontrolComponent } from './leafcontrol/leafcontrol/leafcontrol.component';
import { CultivationworkComponent } from './cultivationwork/cultivationwork/cultivationwork.component';
import { LeavemanagementComponent } from './leavemanagement/leavemanagement/leavemanagement.component';
import { GardenweatherComponent } from './gardenweather/gardenweather/gardenweather.component';
import { LoginComponent } from './auth/login/login/login.component';
import { DivisionmanagementComponent } from './divisionmanagement/divisionmanagement/divisionmanagement.component';
import { PluckingtaskallocationComponent } from './pluckingtaskallocation/pluckingtaskallocation/pluckingtaskallocation.component';
import { ReportViewComponent } from './report-view/report-view.component';
import { TestComponent } from './test/test.component';
import { AttendenceDailyReportComponent } from './report-view/attendence-daily-report/attendence-daily-report.component';
import { WagessummaryreportComponent } from './report/wagessummaryreport/wagessummaryreport.component';
import { DailyattendencereportComponent } from './report-view/dailyattendencereport/dailyattendencereport.component';
import { FileuploadComponent } from './manualentry/fileupload/fileupload.component';
import { PfandabsentreportComponent } from './report-view/pfandabsentreport/pfandabsentreport.component';
import { masterinformationComponent } from './report-view/masterinformation/masterinformation.component';
import { DeviceregistrationComponent } from './deviceregistration/deviceregistration/deviceregistration.component';
import { SalaryconfigurationComponent } from './salaryconfiguration/salaryconfiguration.component';
import { transactionComponent } from './report-view/transaction/transaction.component';
import { OperatorpermissionComponent } from './operatorpermission/operatorpermission/operatorpermission.component';
import { PermissionGuard } from './guard/permission.guard';
import { DailyAttendanceDetailsComponent } from './report-view/dailyattendancedetails/dailyattendancedetails.component';
import { BonusSummaryComponent } from './report-view/bonussummary/bonussummary/bonussummary.component';
import { WorkerWiseAttendanceDetailsComponent } from './report-view/workerwiseattendancedetails/workerwiseattendancedetails.component';
import { WorkerWiseAttendanceSummaryComponent } from './report-view/workerwiseattendancesummary/workerwiseattendancesummary.component';
import { DailyGardenSummaryComponent } from './report-view/dailygardensummary/dailygardensummary.component';
import { LeaveSummaryComponent } from './report-view/leavesummary/leavesummary.component';
import { AbsentSummaryComponent } from './report-view/absentsummary/absentsummary.component';
import { PfSummaryComponent } from './report-view/pfsummary/pfsummary.component';
import { GreenLeafSummaryComponent } from './report-view/greenleafsummary/greenleafsummary.component';
const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'usermanagement',
    component: UsermanagementComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'companymanagement',
    component: CompanymanagementComponent,
    canActivate: [PermissionGuard],
  },

  { path: 'Report', component: ReportViewComponent },
  { path: 'Wagessummaryreport', component: WagessummaryreportComponent },

  {
    path: 'gardenmanagement',
    component: GardenmanagementComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'kamjarimanagement',
    component: KamjarimanagementComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'shiftmanagement',
    component: ShiftmanagementComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'workermanagement',
    component: WorkermanagementComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'holiday',
    component: HolidayComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'sectionmanagement',
    component: SectionmanagementComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'pluckingtypemanagement',
    component: PluckingtypemanagementComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'prunetypemanagement',
    component: PrunetypemanagementComponent,
    canActivate: [PermissionGuard],
  },
  { path: 'configuration', component: ConfigurationComponent },
  {
    path: 'wagecalender',
    component: WagecalenderComponent,
    canActivate: [PermissionGuard],
  },
  { path: 'rationmanagement', component: RationmanagementComponent },
  {
    path: 'dailyplucking',
    component: DailypluckingComponent,
    canActivate: [PermissionGuard],
  },

  {
    path: 'nonpluckingactivities',
    component: NonpluckingactivitiesComponent,
    canActivate: [PermissionGuard],
  },
  { path: 'leafcontrol', component: LeafcontrolComponent },
  { path: 'cultivationwork', component: CultivationworkComponent },
  {
    path: 'leavemanagement',
    component: LeavemanagementComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'pluckingtaskallocation',
    component: PluckingtaskallocationComponent,
    canActivate: [PermissionGuard],
  },
  { path: 'gardenweather', component: GardenweatherComponent },
  {
    path: 'division',
    component: DivisionmanagementComponent,
    canActivate: [PermissionGuard],
  },
  { path: 'test', component: TestComponent },
  {
    path: 'file',
    component: FileuploadComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'daterangewisereport',
    component: PfandabsentreportComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'masterinformation',
    component: masterinformationComponent,
  },
  {
    path: 'transaction',
    component: transactionComponent,
  },
  {
    path: 'deviceregistration',
    component: DeviceregistrationComponent,
    canActivate: [PermissionGuard],
  },
  { path: 'salaryconfiguration', component: SalaryconfigurationComponent },
  {
    path: 'attendancedetailsreport',
    component: AttendenceDailyReportComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'dailyattendancereport',
    component: DailyattendencereportComponent,
    canActivate: [PermissionGuard],
  },
  { path: 'login', component: LoginComponent },
  {
    path: 'operatorpermission',
    component: OperatorpermissionComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'dailyattendancedetails',
    component: DailyAttendanceDetailsComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'bonussummary',
    component: BonusSummaryComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'workerwiseattendancedetails',
    component: WorkerWiseAttendanceDetailsComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'workerwiseattendancesummary',
    component: WorkerWiseAttendanceSummaryComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'dailygardensummary',
    component: DailyGardenSummaryComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'leavesummary',
    component: LeaveSummaryComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'absentsummary',
    component: AbsentSummaryComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'pfsummary',
    component: PfSummaryComponent,
    canActivate: [PermissionGuard],
  },
  {
    path: 'greenleafsummary',
    component: GreenLeafSummaryComponent,
    canActivate: [PermissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
