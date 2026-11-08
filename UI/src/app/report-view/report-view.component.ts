import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { CommonService } from '../service/common/common.service';
import { ConnectionService } from '../service/connection/connection.service';

@Component({
  selector: 'app-report-view',
  templateUrl: './report-view.component.html',
  styleUrl: './report-view.component.css',
})
export class ReportViewComponent {
  iframeUrl: SafeResourceUrl = '';
  ReportUrls: any;
  ReportCode: any;

  constructor(
    private sanitizer: DomSanitizer,
    private common: CommonService,
    private connection: ConnectionService
  ) {}

  ngOnInit(): void {
    this.ReportCode = localStorage.getItem('selectedMenuId');
    this.toggleforid();
  }
  toggleforid() {
    this.common.GetReport(this.ReportCode).subscribe((data) => {
      console.log(this.ReportCode);
      this.ReportUrls = data;
      const motherUrl = this.connection.ReportUrl;

      const folderUrl = this.ReportUrls.folderurl;
      const otherUrl = this.ReportUrls.otherurl;
      const unit = localStorage.getItem('LoginUnit');
      const uniturt = '&UNITID=' + unit;

      const fullUrl = `${motherUrl}${encodeURIComponent(
        folderUrl
      )}${uniturt}${otherUrl}`;

      this.iframeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(fullUrl);
    });
  }
}
