import { ApiResponse } from './api-response';

describe('ApiResponse', () => {
  it('should create an instance', () => {
    expect(new ApiResponse({ success: 1, data: {} })).toBeTruthy();
  });

  it('should throw error for failed request', () => {
    expect(() => {
      new ApiResponse({ asdf: 'asdf' });
    }).toThrow(new Error('API failure: invalid response'));

    expect(() => {
      new ApiResponse({});
    }).toThrow(new Error('API failure: invalid response'));

    expect(() => {
      new ApiResponse({ success: 0, data: null });
    }).toThrow(new Error('API failure: invalid response'));

    expect(() => {
      new ApiResponse({ success: 0, data: 'test error message' });
    }).toThrow(new Error('API failure: test error message'));
  });
});
