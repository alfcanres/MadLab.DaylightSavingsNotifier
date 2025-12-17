import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppPager } from './app-pager';

describe('AppPager', () => {
  let component: AppPager;
  let fixture: ComponentFixture<AppPager>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppPager]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppPager);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
