import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SideMenuComponent } from './core/components/sidemenu/sidemenu.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, SideMenuComponent,
    MatToolbarModule, MatButtonModule, MatIconModule, MatSidenavModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class AppComponent implements OnDestroy {
  
  title = 'StLukesMedicalApp.UI';
  public isMobile = false;
  private breakpointSubscription: Subscription;

  constructor(private breakpointObserver: BreakpointObserver) {
    this.breakpointSubscription = this.breakpointObserver.observe([
      Breakpoints.Handset
    ]).subscribe({
      next: result => this.isMobile = result.matches,
      error: err => { console.error('Error observing breakpoints', err)}
    });
  }

  ngOnDestroy(): void {
    this.breakpointSubscription.unsubscribe();
  }
}