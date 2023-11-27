import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReporteCuartosComponent } from './reporte-cuartos.component';

describe('ReporteCuartosComponent', () => {
  let component: ReporteCuartosComponent;
  let fixture: ComponentFixture<ReporteCuartosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReporteCuartosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReporteCuartosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
