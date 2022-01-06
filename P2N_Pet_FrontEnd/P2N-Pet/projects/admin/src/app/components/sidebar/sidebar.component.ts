import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
  { path: '/admin/dashboard', title: 'Thống kê',  icon: 'ni-tv-2 text-primary', class: '' },
  { path: '/admin/list-order', title: 'Đơn hàng',  icon:'ni ni-cart text-red', class: '' },
  { path: '/admin/list-pet', title: 'Nguồn gốc thú cưng',  icon:'fa fa-paw text-yellow', class: '' },
  { path: '/admin/list-petdetail', title: 'Thông tin thú cưng', icon:'ni-collection text-info', class:''}
];

export const PETDETAILS: RouteInfo[] = [
  { path: '/admin/list-breed', title: 'Loại thú cưng',  icon:'ni ni-books text-blue', class: '' },
  { path: '/admin/list-age', title: 'Tuổi hiện tại',  icon:'fa fa-spinner text-violet', class: '' },
  { path: '/admin/list-color', title: 'Màu sắc',  icon:'ni ni-palette text-green', class: '' },
  { path: '/admin/list-size', title: 'Kích cỡ',  icon:'fa fa-sort text-pink', class: '' },
  { path: '/admin/list-supplier', title: 'Nhà cung cấp',  icon:'fa fa-university text-red', class: '' },
];

export const PETEXTRAS: RouteInfo[] = [
  { path: '/admin/list-promotion', title: 'Quảng cáo',  icon:'ni ni-world-2 text-info', class: '' },
  { path: '/admin/list-customer', title: 'Khách hàng',  icon:'ni ni-briefcase-24 text-orange', class: '' },
  { path: '/admin/list-contact', title: 'Liên hệ',  icon:'ni ni-chat-round text-blue', class: '' },
];

export const ADMINS: RouteInfo[] = [
  { path: '/admin/list-account', title: 'Tài khoản',  icon:'ni ni-single-02 text-red', class: '' },
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public petDetails: any[];
  public petExtras: any[];
  public admins: any[];
  public isCollapsed = true;

  user: any;

  constructor(private router: Router,
    private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.getProfile().subscribe((output: any) => {
      this.user = output.content.user;

      if(this.user && this.user.RoleId == 20){
          this.menuItems = ROUTES.filter(menuItem => menuItem);
          this.petDetails = PETDETAILS.filter(menuItem => menuItem);
          this.petExtras = PETEXTRAS.filter(menuItem => menuItem);
      }
      else if(this.user && this.user.RoleId == 10){
        this.admins = ADMINS.filter(menuItem => menuItem);
      }
  });

    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }
}
