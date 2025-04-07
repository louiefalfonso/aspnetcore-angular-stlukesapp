import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmissionListComponent } from './admission-list.component';

describe('AdmissionListComponent', () => {
  let component: AdmissionListComponent;
  let fixture: ComponentFixture<AdmissionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdmissionListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmissionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
