import { EventEmitter, Injectable } from "@angular/core";

@Injectable()
export class EventBusService{
	FormResponseHandled : EventEmitter<any> = new EventEmitter();
	actionListRunEvent : EventEmitter<any> = new EventEmitter(); 
	constructor() {
	}
	createEvent(name, args){
		
	}
}