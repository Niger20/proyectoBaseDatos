import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendedorClientesComponent } from './vendedor-clientes.component';

describe('VendedorClientesComponent', () => {
  let component: VendedorClientesComponent;
  let fixture: ComponentFixture<VendedorClientesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VendedorClientesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VendedorClientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
