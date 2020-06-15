export class ApiResponse {
  data: any;

  constructor(rawApiResponse: object) {
    if (rawApiResponse['success'] === 1) {
      Object.assign(this, rawApiResponse);
    } else {
      throw new Error('API failure: ' + rawApiResponse['data']);
    }
  }
}
