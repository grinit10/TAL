import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { TimeSlot } from 'src/app/shared/models/timeSlot.model';
import { GetTimeSlotsResult } from 'src/app/store/timeSlots/actions/timeSlot.actions';

@Component({
  selector: 'app-slots-list',
  templateUrl: './slots-list.component.html',
  styleUrls: ['./slots-list.component.scss']
})
export class SlotsListComponent implements OnInit {

  timeSlots: TimeSlot[] = [];
  isLoading = false;

  constructor(public store: Store) {
    this.store.select(state => state.timeSlot.items).subscribe((r: TimeSlot[]) => {
      this.timeSlots = r;
    });

    this.store.select(state => state.temperature.isLoading).subscribe((r: boolean) => {
      this.isLoading = r;
    });
   }

  ngOnInit(): void {
    this.store.dispatch(new GetTimeSlotsResult());
  }

  isSlotAvailable = (timeSlot: TimeSlot): boolean =>
  timeSlot.isAvailable

}
