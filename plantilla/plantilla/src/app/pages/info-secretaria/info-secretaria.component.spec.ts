import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoSecretariaComponent } from './info-secretaria.component';

describe('InfoSecretariaComponent', () => {
  let component: InfoSecretariaComponent;
  let fixture: ComponentFixture<InfoSecretariaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InfoSecretariaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InfoSecretariaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
