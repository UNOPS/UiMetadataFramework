
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { FormMetadata } from "uimf-core";
import { Injectable } from "@angular/core";
import { MetadataService } from "./services/metadata.service";

@Injectable()
export class FormResolver implements Resolve<FormMetadata> {
    form: FormMetadata;
    constructor(private metadataService: MetadataService) { }

    resolve(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<FormMetadata> {
        
          this.metadataService.metadata.subscribe((metadata) => this.form = metadata.find(f => f.id == route.params['id']));
           return Observable.of(this.form);
    }

}