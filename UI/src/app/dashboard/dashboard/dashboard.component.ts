import { Component, OnInit } from '@angular/core';
import {
  DashboardService,
  DashboardStats,
  SectionPlucking,
  PluckingTrend,
} from '../../service/dashboard/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
  stats?: DashboardStats;
  loading = false;
  selectedDate: string = this.today();

  // Derived plucking delta
  plkDiff = 0;
  plkPct = 0;
  isUp = true;

  sections: SectionPlucking[] = [];

  // Attendance pie chart config (ApexCharts)
  simplePieChart: any;
  trendChart: any;
  pieTotal = 0;

  constructor(public service: DashboardService) {}

  ngOnInit(): void {
    this.load();
  }

  private today(): string {
    // yyyy-MM-dd in local time (avoids UTC off-by-one from toISOString)
    const d = new Date();
    const off = d.getTimezoneOffset();
    return new Date(d.getTime() - off * 60000).toISOString().slice(0, 10);
  }

  onDateChange(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.service.GetDashboardStats(this.selectedDate).subscribe({
      next: (data) => {
        this.stats = data;
        this.computeDelta();
        this.buildPie();
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      },
    });
    this.service.GetSectionPlucking(this.selectedDate).subscribe({
      next: (rows) => (this.sections = rows ?? []),
      error: () => (this.sections = []),
    });
    this.service.GetPluckingTrend(this.selectedDate).subscribe({
      next: (rows) => this.buildTrend(rows),
      error: () => {},
    });
  }

  get sectionSize(): 'lg' | 'md' | 'sm' {
    const n = this.sections.length;
    if (n <= 6) return 'lg';
    if (n <= 16) return 'md';
    return 'sm';
  }

  private computeDelta(): void {
    if (!this.stats) return;
    this.plkDiff = this.stats.todayplk - this.stats.prevdayplk;
    this.plkPct = this.stats.prevdayplk
      ? (this.plkDiff / this.stats.prevdayplk) * 100
      : 0;
    this.isUp = this.plkDiff >= 0;
  }

  private buildPie(): void {
    if (!this.stats) return;
    this.pieTotal =
      Number(this.stats.pieleave) +
      Number(this.stats.pieabsent) +
      Number(this.stats.piepresent);
    const colors = this.getChartColorsArray([
      '#0d6efd', // Blue
      '#ff0000', // Pure Red
      '#0ab39c', // Green
    ]);
    const total =
      this.stats.pieleave + this.stats.pieabsent + this.stats.piepresent;

    this.simplePieChart = {
      series: [
        Number(this.stats.pieleave),
        Number(this.stats.pieabsent),
        Number(this.stats.piepresent),
      ],
      chart: {
        height: 296,
        type: 'donut',
        fontFamily: 'inherit',
        animations: {
          enabled: true,
          easing: 'easeinout',
          speed: 600,
          animateGradually: { enabled: true, delay: 120 },
        },
      },
      labels: ['Leave', 'Absent', 'Present'],
      colors: colors,
      stroke: { width: 2, colors: ['#fff'] },
      plotOptions: {
        pie: {
          expandOnClick: true,
          donut: {
            size: '60%',
            labels: {
              show: false,
            },
          },
        },
      },
      dataLabels: {
        enabled: true,
        style: { fontSize: '13px', fontWeight: 600 },
        dropShadow: { enabled: false },
        formatter: (val: number) => `${val.toFixed(1)}%`,
      },
      legend: {
        position: 'bottom',
        horizontalAlign: 'center',
        fontSize: '13px',
        markers: { width: 10, height: 10, radius: 12 },
        itemMargin: { horizontal: 10, vertical: 4 },
      },
      tooltip: {
        y: {
          formatter: (val: number) => `${val} worker${val === 1 ? '' : 's'}`,
        },
      },
      responsive: [
        {
          breakpoint: 480,
          options: { chart: { height: 280 }, legend: { position: 'bottom' } },
        },
      ],
    };
  }

  private getChartColorsArray(colors: any) {
    return colors.map(function (value: any) {
      var newValue = value.replace(' ', '');
      if (newValue.indexOf(',') === -1) {
        var color = getComputedStyle(document.documentElement).getPropertyValue(
          newValue,
        );
        if (color) {
          color = color.replace(' ', '');
          return color;
        } else return newValue;
      } else {
        var val = value.split(',');
        if (val.length == 2) {
          var rgbaColor = getComputedStyle(
            document.documentElement,
          ).getPropertyValue(val[0]);
          rgbaColor = 'rgba(' + rgbaColor + ',' + val[1] + ')';
          return rgbaColor;
        } else {
          return newValue;
        }
      }
    });
  }

  private buildTrend(rows: PluckingTrend[]): void {
    const colors = this.getChartColorsArray(['--vz-success']);
    const categories = rows.map((r) => {
      const d = new Date(r.pluckdate);
      return d.toLocaleDateString('en-GB', { day: '2-digit', month: 'short' });
    });
    const values = rows.map((r) => Number(r.totqty));

    this.trendChart = {
      series: [{ name: 'Plucked (kg)', data: values }],
      chart: {
        type: 'bar',
        height: 250,
        fontFamily: 'inherit',
        toolbar: { show: false },
        animations: { enabled: true, easing: 'easeinout', speed: 500 },
      },
      plotOptions: {
        bar: {
          borderRadius: 6,
          columnWidth: '45%',
          dataLabels: { position: 'top' },
        },
      },
      colors: colors,
      dataLabels: {
        enabled: true,
        formatter: (val: number) => this.formatKg(val),
        offsetY: -18,
        style: { fontSize: '11px', colors: ['#878a99'], fontWeight: 600 },
      },
      xaxis: {
        categories: categories,
        labels: { style: { fontSize: '12px' } },
        axisBorder: { show: false },
        axisTicks: { show: false },
      },
      yaxis: {
        labels: {
          formatter: (val: number) => this.formatKg(val),
          style: { fontSize: '12px' },
        },
      },
      grid: {
        borderColor: '#e9ebec',
        strokeDashArray: 4,
        yaxis: { lines: { show: true } },
        xaxis: { lines: { show: false } },
      },
      tooltip: {
        y: { formatter: (val: number) => `${val.toLocaleString()} kg` },
      },
      fill: { opacity: 1 },
    };
  }

  private formatKg(val: number): string {
    if (val >= 1000) return (val / 1000).toFixed(1) + 'k';
    return `${Math.round(val)}`;
  }
}
