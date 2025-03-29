import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NurseListComponent } from './nurse-list.component';

describe('NurseListComponent', () => {
  let component: NurseListComponent;
  let fixture: ComponentFixture<NurseListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NurseListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NurseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
