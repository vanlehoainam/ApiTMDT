import { Component, OnInit } from '@angular/core';
import { HocVanService } from '../hoc-van.service';

@Component({
  selector: 'app-hoc-van-list',
  templateUrl: './hoc-van-list.component.html',
  styleUrls: ['./hoc-van-list.component.css']
})
export class HocVanListComponent implements OnInit {

  hocVans: any[] = [];
  loading: boolean = true;

  constructor(private hocVanService: HocVanService) { }

  ngOnInit(): void {
    this.hocVanService.getHocVans().subscribe(
      (data) => {
        this.hocVans = data;
        this.loading = false;
      },
      (error) => {
        console.error("Có lỗi xảy ra khi tải dữ liệu:", error);
        this.loading = false;
      }
    );
  }
}
