import { Component } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ConnectionService } from '../../service/connection/connection.service';
import { CommonService } from '../../service/common/common.service';

@Component({
  selector: 'app-wagessummaryreport',
  templateUrl: './wagessummaryreport.component.html',
  styleUrl: './wagessummaryreport.component.css'
})
export class WagessummaryreportComponent {
iframeUrl: SafeResourceUrl = '';
  ReportUrls: any;
  ReportCode:any;

  constructor(
    private sanitizer: DomSanitizer,
    private common:CommonService,
    private connection:ConnectionService
  ) {}

  ngOnInit(): void {

  

    this.ReportCode=localStorage.getItem('selectedMenuId') ;
   this.toggleforid();
    

  }  
  toggleforid(){
   this.common.GetReportWS(this.ReportCode).subscribe(data=>{
     
            this.ReportUrls = data;
            const motherUrl = this.connection.ReportUrl;
          
            const folderUrl = this.ReportUrls.folderurl;
            const otherUrl = this.ReportUrls.otherurl;
            const unit = localStorage.getItem('LoginUnit');
            const uniturt="&UNITID="+unit
            const fullUrl = `${motherUrl}${encodeURIComponent(folderUrl)}${uniturt}${otherUrl}`;
            this.iframeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(fullUrl);
    });
  }
}
