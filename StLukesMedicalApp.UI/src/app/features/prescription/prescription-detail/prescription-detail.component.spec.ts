import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrescriptionDetailComponent } from './prescription-detail.component';

describe('PrescriptionDetailComponent', () => {
  let component: PrescriptionDetailComponent;
  let fixture: ComponentFixture<PrescriptionDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PrescriptionDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrescriptionDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
