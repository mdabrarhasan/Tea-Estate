import { Component, OnInit } from '@angular/core';
import { TestService } from '../service/pagination/test.service';
import { KamjarimanagementService } from '../service/kamjarimanagement/kamjarimanagement.service';
import { PaginationService } from '../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../service/common/common.service';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrl: './test.component.css'
})

export class TestComponent implements OnInit {
    breadCrumbItems!: Array<{}>;
    searchText:string='';
    simplePieChart: any;
    simpleDonutChart: any;
    updatingDonutChart: any;
    monochromePieChart: any;
    gradientDonutChart: any;
    patternedDonutChart: any;
    semiDonutChart: any;
    pieWithImageChart: any;

    items: any[] = [];
    pageOfItems?: Array<any>;
    sortProperty: string = 'id';
    sortOrder = 1;
    loading = false;
    FirstName:any;
    data:any;
    tempitems:any;

    constructor(public pagination:TestService,public service: KamjarimanagementService,private sortService: PaginationService,private modalService: NgbModal,private cs:CommonService,private toastr: ToastrService) {
          this.Loadlist();
        }

        
        Loadlist()
        {
          this.service.GetData().subscribe((data: any[])=>{
                this.items=data;
                this.tempitems=data;
            })   
        }

        filterData() {
          const text = this.searchText.toLowerCase().trim();

          if (text) {
            this.items = this.items.filter(item =>item.name.toLowerCase().includes(text)
            );
          } else {
            this.items = [...this.tempitems]; // Reset to original
          }
        }
  

        onChangePage(pageOfItems: Array<any>) {
          this.pageOfItems = pageOfItems;
        }
  
      sortBy(property: string) {
          this.sortOrder = property === this.sortProperty ? (this.sortOrder * -1) : 1;
          this.sortProperty = property;
          this.items = [...this.items.sort((a: any, b: any) => {
              // sort comparison function
              let result = 0;
              if (a[property] < b[property]) {
                  result = -1;
              }
              if (a[property] > b[property]) {
                  result = 1;
              }
              return result * this.sortOrder;
          })];
      }
  
      sortIcon(property: string) {
          if (property === this.sortProperty) {
              return this.sortOrder === 1 ? '☝️' : '👇';
          }
          return '';
      }     
        
  ngOnInit(): void {

    this._simplePieChart('["--vz-primary", "--vz-success", "--vz-warning", "--vz-danger", "--vz-info"]');
    this._simpleDonutChart('["--vz-primary", "--vz-success", "--vz-warning", "--vz-danger", "--vz-info"]');
    this._updatingDonutChart('["--vz-primary", "--vz-primary-rgb, 0.80", "--vz-primary-rgb, 0.60", "--vz-primary-rgb, 0.45"]');
    this._monochromePieChart('["--vz-primary"]');
    this._gradientDonutChart('["--vz-primary", "--vz-success", "--vz-warning", "--vz-danger", "--vz-info"]');
    this._patternedDonutChart('["--vz-primary", "--vz-success", "--vz-warning", "--vz-danger", "--vz-info"]');
    this._pieWithImageChart('["--vz-primary", "--vz-success", "--vz-warning", "--vz-info"]');
  }


private getChartColorsArray(colors:any) {
  colors = JSON.parse(colors);
  return colors.map(function (value:any) {
    var newValue = value.replace(" ", "");
    if (newValue.indexOf(",") === -1) {
      var color = getComputedStyle(document.documentElement).getPropertyValue(newValue);
          if (color) {
          color = color.replace(" ", "");
          return color;
          }
          else return newValue;;
      } else {
          var val = value.split(',');
          if (val.length == 2) {
              var rgbaColor = getComputedStyle(document.documentElement).getPropertyValue(val[0]);
              rgbaColor = "rgba(" + rgbaColor + "," + val[1] + ")";
              return rgbaColor;
          } else {
              return newValue;
          }
      }
  });
}

/**
* Simple Pie Chart
*/
private _simplePieChart(colors:any) {
  colors = this.getChartColorsArray(colors);
  this.simplePieChart = {
    series: [44, 55, 13, 43, 22],
    chart: {
      height: 300,
      type: "pie",
    },
    labels: ["Team A", "Team B", "Team C", "Team D", "Team E"],
    legend: {
      position: "bottom",
    },
    dataLabels: {
      dropShadow: {
        enabled: false,
      },
    },
    colors: colors,
  };
}


private _simpleDonutChart(colors:any) {
  colors = this.getChartColorsArray(colors);
  this.simpleDonutChart = {
    series: [44, 55, 41, 17, 15],
    chart: {
      height: 300,
      type: "donut",
    },
    legend: {
      position: "bottom",
    },
    dataLabels: {
      dropShadow: {
        enabled: false,
      },
    },
    colors: colors,
  };
}

// Updating Donut Charts
private _updatingDonutChart(colors:any) {
  colors = this.getChartColorsArray(colors);
  this.updatingDonutChart = {
    series: [44, 55, 13, 33],
    chart: {
      height: 280,
      type: "donut",
    },
    dataLabels: {
      enabled: false,
    },
    legend: {
      position: "bottom",
    },
    colors: colors,
  };
}

/**
* Monochrome Pie Chart
*/
private _monochromePieChart(colors:any) {
  colors = this.getChartColorsArray(colors);    
  this.monochromePieChart = {
    series: [25, 15, 44, 55, 41, 17],
    chart: {
      height: 300,
      type: "pie",
    },
    labels: [
      "Monday",
      "Tuesday",
      "Wednesday",
      "Thursday",
      "Friday",
      "Saturday",
    ],
    theme: {
      monochrome: {
        enabled: true,
        color: "#405189",
        shadeTo: "light",
        shadeIntensity: 0.6,
      },
    },
    plotOptions: {
      pie: {
        dataLabels: {
          offset: -5,
        },
      },
    },
    title: {
      text: "Monochrome Pie",
      style: {
        fontWeight: 500,
      },
    },
    dataLabels: {
      dropShadow: {
        enabled: false,
      },
    },
    legend: {
      show: false,
    },
  };
}

/**
* Gradient Donut Chart
*/
private _gradientDonutChart(colors:any) {
  colors = this.getChartColorsArray(colors);
  this.gradientDonutChart = {
    series: [44, 55, 41, 17, 15],
    chart: {
      height: 300,
      type: "donut",
    },
    plotOptions: {
      pie: {
        startAngle: -90,
        endAngle: 270,
      },
    },
    dataLabels: {
      enabled: false,
    },
    fill: {
      type: "gradient",
    },
    legend: {
      position: "bottom",
      formatter: function(val:any, opts:any) {
        return val + " - " + opts.w.globals.series[opts.seriesIndex];
      },
    },
    title: {
      text: "Gradient Donut with custom Start-angle",
      style: {
        fontWeight: 500,
      },
    },
    colors: colors,
  };
}

/**
* Patterned Donut Chart
*/
private _patternedDonutChart(colors:any) {
  colors = this.getChartColorsArray(colors);
  this.patternedDonutChart = {
  series: [44, 55, 41, 17, 15],
  chart: {
    height: 300,
    type: "donut",
    dropShadow: {
      enabled: true,
      color: "#111",
      top: -1,
      left: 3,
      blur: 3,
      opacity: 0.2,
    },
  },
  stroke: {
    width: 0,
  },
  plotOptions: {
    pie: {
      donut: {
        labels: {
          show: true,
          total: {
            showAlways: true,
            show: true,
          },
        },
      },
    },
  },
  labels: ["Comedy", "Action", "SciFi", "Drama", "Horror"],
  dataLabels: {
    dropShadow: {
      blur: 3,
      opacity: 0.8,
    },
  },
  fill: {
    type: "pattern",
    opacity: 1,
    pattern: {
      style: [
        "verticalLines",
        "squares",
        "horizontalLines",
        "circles",
        "slantedLines",
      ],
    },
  },
  states: {
    hover: {
    },
  },
  theme: {
    palette: "palette2",
  },
  title: {
    text: "Favourite Movie Type",
    style: {
      fontWeight: 500,
    },
  },
  legend: {
    position: "bottom",
  },
  colors: colors,
  };
}

/**
* Pie Chart with Image Fill
*/
private _pieWithImageChart(colors:any) {
colors = this.getChartColorsArray(colors);
this.pieWithImageChart = {
  series: [44, 33, 54, 45],
  chart: {
    height: 300,
    type: "pie",
  },
  colors: colors,
  fill: {
    type: "image",
    opacity: 0.85,
    image: {
      src: [
        '../../../../../assets/images/small/img-3.jpg', '../../../../../assets/images/small/img-5.jpg', '../../../../../assets/images/small/img-2.jpg',
      ],
      width: 25,
    },
  },
  stroke: {
    width: 4,
  },
  dataLabels: {
    enabled: true,
    style: {
      colors: ["#111"],
    },
    background: {
      enabled: true,
      foreColor: "#fff",
      borderWidth: 0,
    },
  },
  legend: {
    position: "bottom",
  },
};
}
       
 /**
   * Default Select2
   */
  selectedAccount = 'This is a placeholder';
  Default = [
    { name: 'Choice 1' },
    { name: 'Choice 2' },
    { name: 'Choice 3' },
  ];

  /**
   * Option groups Select2
   */
   selectedGroup = 'Choose a city';
   Groups = [
     { name: 'Montreal', country: 'CA', child: { state: 'Active' } },
     { name: 'Toronto', country: 'CA', child: { state: 'Active' } },
     { name: 'Vancouver', country: 'CA', child: { state: 'Active' } },
     { name: 'Lyon', country: 'FR', child: { state: 'Active' } },
     { name: 'Marseille', country: 'FR', child: { state: 'Active' } },
     { name: 'Paris', country: 'FR', child: { state: 'Active' } },
     { name: 'Barcelona', country: 'SP', child: { state: 'Active' } },
     { name: 'Madrid', country: 'SP', child: { state: 'Active' } },
     { name: 'Liverpool', country: 'UK', child: { state: 'Active' } },
     { name: 'London', country: 'UK', child: { state: 'Active' } },
     { name: 'Manchester', country: 'UK', child: { state: 'Active' } },
     { name: 'Michigan', country: 'US', child: { state: 'Active' } },
     { name: 'New York', country: 'US', child: { state: 'Active' } },
     { name: 'Washington', country: 'US', child: { state: 'Inactive' } }
   ];

   /**
  * Option Disabled groups Select2
  */
    selectedOption = 'Label Six';
    Options = [
      { name: 'Label Five' },
      { name: 'Label Four', 'disabled': true },
      { name: 'Label One' },
      { name: 'Label Six' },
      { name: 'Label Three' },
      { name: 'Label Two', 'disabled': true },
      { name: 'Zero' }];

    /**
  * Option Disabled groups Select2
  */
  searchselectedOption = 'Label Six';
  searchOptions = [
    { name: 'Label Five' },
    { name: 'Label Four', 'disabled': true },
    { name: 'Label One' },
    { name: 'Label Six' },
    { name: 'Label Three' },
    { name: 'Label Two', 'disabled': true },
    { name: 'Zero' }];

  /**
   * Multiple Default Select2
   */
   multiDefaultOption = 'Adam';

    /**
  * Multiple Default Select2
  */
  selectValue = ['Alaska', 'Hawaii', 'California', 'Nevada', 'Oregon', 'Washington', 'Arizona', 'Colorado', 'Idaho', 'Montana', 'Nebraska', 'New Mexico', 'North Dakota', 'Utah', 'Wyoming', 'Alabama', 'Arkansas', 'Illinois', 'Iowa'];

   /**
   * Disabled Select2
   */
    disable = true;
    selectedPeople = [
      { name: 'josh@joshuajohnson.co.uk', disabled: true },
      { name: 'joe@bloggs.co.uk', disabled: true }
    ];

    /**
   * Show Code Toggle
   */
   ShowCode(event: any) {     
    let card = event.target.closest('.card');
    const preview = card.children[1].children[1];
    const codeView = card.children[1].children[2];
    if(codeView != null){
      codeView.classList.toggle('d-none');
    }
    if(preview != null){
      preview.classList.toggle('d-none');
      
    }
  }
     
}
