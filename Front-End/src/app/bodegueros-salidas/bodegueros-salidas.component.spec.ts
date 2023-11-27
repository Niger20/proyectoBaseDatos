import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BodeguerosSalidasComponent } from './bodegueros-salidas.component';

describe('BodeguerosSalidasComponent', () => {
  let component: BodeguerosSalidasComponent;
  let fixture: ComponentFixture<BodeguerosSalidasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BodeguerosSalidasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BodeguerosSalidasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
