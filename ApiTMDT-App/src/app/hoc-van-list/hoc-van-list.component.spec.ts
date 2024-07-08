import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HocVanListComponent } from './hoc-van-list.component';

describe('HocVanListComponent', () => {
  let component: HocVanListComponent;
  let fixture: ComponentFixture<HocVanListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HocVanListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HocVanListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
