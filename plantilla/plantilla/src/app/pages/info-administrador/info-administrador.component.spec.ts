import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoAdministradorComponent } from './info-administrador.component';

describe('InfoAdministradorComponent', () => {
  let component: InfoAdministradorComponent;
  let fixture: ComponentFixture<InfoAdministradorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InfoAdministradorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InfoAdministradorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
