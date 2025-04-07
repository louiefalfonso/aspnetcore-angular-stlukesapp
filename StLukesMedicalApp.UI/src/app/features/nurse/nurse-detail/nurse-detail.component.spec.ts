import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NurseDetailComponent } from './nurse-detail.component';

describe('NurseDetailComponent', () => {
  let component: NurseDetailComponent;
  let fixture: ComponentFixture<NurseDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NurseDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NurseDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
