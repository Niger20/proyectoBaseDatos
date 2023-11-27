import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComprasProveedoresComponent } from './compras-proveedores.component';

describe('ComprasProveedoresComponent', () => {
  let component: ComprasProveedoresComponent;
  let fixture: ComponentFixture<ComprasProveedoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComprasProveedoresComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ComprasProveedoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
