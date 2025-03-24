import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {MatListModule} from '@angular/material/list';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidemenu',
  imports: [MatListModule, RouterLink, CommonModule],
  templateUrl: './sidemenu.component.html',
  styleUrl: './sidemenu.component.css'
})

export class SideMenuComponent {
  menuItems = [
    { name: 'Home', link: '/' },
    { name: 'Admissions', link: '/admissions' },
    { name: 'Patients', link: '/admin/patients' },
  ];
}
