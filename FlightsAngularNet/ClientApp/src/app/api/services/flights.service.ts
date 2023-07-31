/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';

import { FlightsRm } from '../models/flights-rm';

@Injectable({ providedIn: 'root' })
export class FlightsService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `searchFlights()` */
  static readonly SearchFlightsPath = '/Flights';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `searchFlights$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchFlights$Plain$Response(
    params?: {
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<FlightsRm>>> {
    const rb = new RequestBuilder(this.rootUrl, FlightsService.SearchFlightsPath, 'get');
    if (params) {
    }

    return this.http.request(
      rb.build({ responseType: 'text', accept: 'text/plain', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<FlightsRm>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `searchFlights$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchFlights$Plain(
    params?: {
    },
    context?: HttpContext
  ): Observable<Array<FlightsRm>> {
    return this.searchFlights$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<FlightsRm>>): Array<FlightsRm> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `searchFlights$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchFlights$Json$Response(
    params?: {
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<FlightsRm>>> {
    const rb = new RequestBuilder(this.rootUrl, FlightsService.SearchFlightsPath, 'get');
    if (params) {
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'text/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<FlightsRm>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `searchFlights$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchFlights$Json(
    params?: {
    },
    context?: HttpContext
  ): Observable<Array<FlightsRm>> {
    return this.searchFlights$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<FlightsRm>>): Array<FlightsRm> => r.body)
    );
  }

}
