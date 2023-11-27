import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InicioBodegueroComponent } from './inicio-bodeguero.component';

describe('InicioBodegueroComponent', () => {
  let component: InicioBodegueroComponent;
  let fixture: ComponentFixture<InicioBodegueroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InicioBodegueroComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InicioBodegueroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
