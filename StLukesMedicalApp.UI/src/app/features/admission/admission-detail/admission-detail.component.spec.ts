import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmissionDetailComponent } from './admission-detail.component';

describe('AdmissionDetailComponent', () => {
  let component: AdmissionDetailComponent;
  let fixture: ComponentFixture<AdmissionDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdmissionDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmissionDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
