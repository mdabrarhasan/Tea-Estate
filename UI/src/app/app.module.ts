import { NgModule } from '@angular/core';

import {
  BrowserModule,
  provideClientHydration,
  withEventReplay,
} from '@angular/platform-browser';

import { CommonModule, DecimalPipe } from '@angular/common';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';

import { RouterModule } from '@angular/router';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { SimplebarAngularModule } from 'simplebar-angular';

import { NgSelectModule } from '@ng-select/ng-select';

import { NgApexchartsModule } from 'ng-apexcharts';

import { NgxSpinnerModule } from 'ngx-spinner';

// Components

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

import { PluckingtaskallocationComponent } from './pluckingtaskallocation/pluckingtaskallocation/pluckingtaskallocation.component';

import { GardenweatherComponent } from './gardenweather/gardenweather/gardenweather.component';

import { MenuComponent } from './menu/menu/menu.component';

import { SubmenuComponent } from './menu/submenu/submenu.component';

import { LoginComponent } from './auth/login/login/login.component';

import { DivisionmanagementComponent } from './divisionmanagement/divisionmanagement/divisionmanagement.component';

import { SalarygenerateComponent } from './salarygenerate/salarygenerate/salarygenerate.component';

import { ReportViewComponent } from './report-view/report-view.component';

import { TestComponent } from './test/test.component';

import { AttendenceDailyReportComponent } from './report-view/attendence-daily-report/attendence-daily-report.component';

import { PaginationComponent } from './pagination/pagination.component';

import { FilterPipe } from './pagination/filter.pipe';

import { WagessummaryreportComponent } from './report/wagessummaryreport/wagessummaryreport.component';

import { DailyattendencereportComponent } from './report-view/dailyattendencereport/dailyattendencereport.component';

import { FileuploadComponent } from './manualentry/fileupload/fileupload.component';

import { PfandabsentreportComponent } from './report-view/pfandabsentreport/pfandabsentreport.component';

import { DeviceregistrationComponent } from './deviceregistration/deviceregistration/deviceregistration.component';

import { SalaryconfigurationComponent } from './salaryconfiguration/salaryconfiguration.component';

import { masterinformationComponent } from './report-view/masterinformation/masterinformation.component';

import { transactionComponent } from './report-view/transaction/transaction.component';

import { authinterceptorInterceptor } from './auth/interceptor/authinterceptor.interceptor';

import { HttpRequestInterceptor } from './auth/interceptor/spinner-interceptor';
import { OperatorpermissionComponent } from './operatorpermission/operatorpermission/operatorpermission.component';
import { HasPermissionDirective } from './directive/has-permission.directive';
import { DailyAttendanceDetailsComponent } from './report-view/dailyattendancedetails/dailyattendancedetails.component';
import { BonusSummaryComponent } from './report-view/bonussummary/bonussummary/bonussummary.component';
import { WorkerWiseAttendanceDetailsComponent } from './report-view/workerwiseattendancedetails/workerwiseattendancedetails.component';
import { WorkerWiseAttendanceSummaryComponent } from './report-view/workerwiseattendancesummary/workerwiseattendancesummary.component';
import { DailyGardenSummaryComponent } from './report-view/dailygardensummary/dailygardensummary.component';
import { LeaveSummaryComponent } from './report-view/leavesummary/leavesummary.component';
import { AbsentSummaryComponent } from './report-view/absentsummary/absentsummary.component';
import { PfSummaryComponent } from './report-view/pfsummary/pfsummary.component';
import { GreenLeafSummaryComponent } from './report-view/greenleafsummary/greenleafsummary.component';
@NgModule({
  declarations: [
    AppComponent,

    DashboardComponent,

    CompanymanagementComponent,

    UsermanagementComponent,

    GardenmanagementComponent,

    KamjarimanagementComponent,

    ShiftmanagementComponent,

    WorkermanagementComponent,

    HolidayComponent,

    SectionmanagementComponent,

    PluckingtypemanagementComponent,

    PrunetypemanagementComponent,

    ConfigurationComponent,

    WagecalenderComponent,

    RationmanagementComponent,

    DailypluckingComponent,

    NonpluckingactivitiesComponent,

    LeafcontrolComponent,

    CultivationworkComponent,

    LeavemanagementComponent,

    PluckingtaskallocationComponent,

    GardenweatherComponent,

    MenuComponent,

    SubmenuComponent,

    LoginComponent,

    DivisionmanagementComponent,

    SalarygenerateComponent,

    ReportViewComponent,

    WagessummaryreportComponent,

    TestComponent,

    AttendenceDailyReportComponent,

    PaginationComponent,

    FilterPipe,

    DailyattendencereportComponent,

    FileuploadComponent,

    PfandabsentreportComponent,

    DeviceregistrationComponent,

    SalaryconfigurationComponent,

    masterinformationComponent,

    transactionComponent,

    OperatorpermissionComponent,

    HasPermissionDirective,
    DailyAttendanceDetailsComponent,
    BonusSummaryComponent,
    WorkerWiseAttendanceDetailsComponent,
    WorkerWiseAttendanceSummaryComponent,
    DailyGardenSummaryComponent,
    LeaveSummaryComponent,
    AbsentSummaryComponent,
    PfSummaryComponent,
    GreenLeafSummaryComponent,
  ],

  imports: [
    BrowserModule,

    CommonModule,

    AppRoutingModule,

    HttpClientModule,

    ReactiveFormsModule,

    FormsModule,

    RouterModule,

    BrowserAnimationsModule,

    ToastrModule.forRoot(),

    NgxSpinnerModule.forRoot({
      type: 'line-spin-clockwise-fade',
    }),

    NgbModule,

    SimplebarAngularModule,

    NgSelectModule,

    NgApexchartsModule,
  ],

  providers: [
    DecimalPipe,

    {
      provide: HTTP_INTERCEPTORS,
      useClass: authinterceptorInterceptor,
      multi: true,
    },

    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpRequestInterceptor,
      multi: true,
    },
  ],

  bootstrap: [AppComponent],
})
export class AppModule {}
