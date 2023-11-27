import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendedorVentasComponent } from './vendedor-ventas.component';

describe('VendedorVentasComponent', () => {
  let component: VendedorVentasComponent;
  let fixture: ComponentFixture<VendedorVentasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VendedorVentasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VendedorVentasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
