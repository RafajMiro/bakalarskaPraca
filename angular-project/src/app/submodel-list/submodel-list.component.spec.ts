import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmodelListComponent } from './submodel-list.component';

describe('SubmodelListComponent', () => {
  let component: SubmodelListComponent;
  let fixture: ComponentFixture<SubmodelListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubmodelListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SubmodelListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
