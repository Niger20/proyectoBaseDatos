import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComprasComprasComponent } from './compras-compras.component';

describe('ComprasComprasComponent', () => {
  let component: ComprasComprasComponent;
  let fixture: ComponentFixture<ComprasComprasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ComprasComprasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ComprasComprasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
