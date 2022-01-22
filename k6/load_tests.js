/*
 * init code
 */
import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  stages: [
    { duration: '5s', target: 10 },
    { duration: '5s', target: 100 },
    { duration: '5s', target: 50 },
  ],
  // vus: 100,
  // duration: '5s',
};

export default function () {
  /*
   * vu code
   */

  http.get('http://test.k6.io');
  sleep(1);
}
