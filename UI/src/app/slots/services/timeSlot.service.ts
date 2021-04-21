import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TimeSlot } from 'src/app/shared/models/timeSlot.model';
import { DataService } from 'src/app/shared/services/data.service';
import { ReadService } from 'src/app/shared/services/read.service';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class TimeSlotService extends DataService {

    constructor(public readService: ReadService,
                public http: HttpClient) {
        super(readService, `${environment.apiBaseUri}/TimeSlots`);
    }

    getTimeSlots = (): Observable<TimeSlot[]> =>
        this.readService.readAll<TimeSlot[]>(null, this.url)
}
