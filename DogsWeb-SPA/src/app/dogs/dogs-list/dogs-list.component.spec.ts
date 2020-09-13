/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DogsListComponent } from './dogs-list.component';

describe('DogsListComponent', () => {
  let component: DogsListComponent;
  let fixture: ComponentFixture<DogsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DogsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DogsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
