import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Counter } from "./models";

@Injectable()
export class CounterService {

  private url = 'api/counters';

  constructor(private http: HttpClient) { }

  getCounters(): Observable<Counter[]> {
    return this.http.get<Counter[]>(this.url);
  }

  getCounter(id: number): Observable<Counter> {
    return this.http.get<Counter>(`${this.url}/${id}`);
  }
}