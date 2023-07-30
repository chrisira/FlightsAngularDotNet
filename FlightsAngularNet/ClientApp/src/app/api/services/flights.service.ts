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

  /** Path part for operation `flightsGet()` */
  static readonly FlightsGetPath = '/Flights';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `flightsGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  flightsGet$Plain$Response(
    params?: {
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<FlightsRm>>> {
    const rb = new RequestBuilder(this.rootUrl, FlightsService.FlightsGetPath, 'get');
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
   * To access the full response (for headers, for example), `flightsGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  flightsGet$Plain(
    params?: {
    },
    context?: HttpContext
  ): Observable<Array<FlightsRm>> {
    return this.flightsGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<FlightsRm>>): Array<FlightsRm> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `flightsGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  flightsGet$Json$Response(
    params?: {
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<FlightsRm>>> {
    const rb = new RequestBuilder(this.rootUrl, FlightsService.FlightsGetPath, 'get');
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
   * To access the full response (for headers, for example), `flightsGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  flightsGet$Json(
    params?: {
    },
    context?: HttpContext
  ): Observable<Array<FlightsRm>> {
    return this.flightsGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<FlightsRm>>): Array<FlightsRm> => r.body)
    );
  }

}
