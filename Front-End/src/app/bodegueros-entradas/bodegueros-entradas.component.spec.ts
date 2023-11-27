import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BodeguerosEntradasComponent } from './bodegueros-entradas.component';

describe('BodeguerosEntradasComponent', () => {
  let component: BodeguerosEntradasComponent;
  let fixture: ComponentFixture<BodeguerosEntradasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BodeguerosEntradasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BodeguerosEntradasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
