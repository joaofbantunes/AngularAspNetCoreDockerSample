import { Component, OnInit } from '@angular/core';
import { CounterService } from '../counter.service';
import { Counter } from '../models/index';

@Component({
  selector: 'app-counter',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.css']
})
export class CounterComponent implements OnInit {

  public counters: Counter[] = [];
  public selectedCounter: Counter;

  constructor(private counterService: CounterService) { }

  ngOnInit() {
    this.counterService.getCounters()
      .subscribe(counters => this.counters = counters);
  }

  onCounterSelected(id: number) {
    this.selectedCounter = null;
    this.counterService.getCounter(id)
      .subscribe(counter => this.selectedCounter = counter);
  }
}