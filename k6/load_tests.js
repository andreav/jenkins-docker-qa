/*
 * init code
 */
import http from 'k6/http';
import { sleep } from 'k6';

// For converting output to jUnit format for jenkins
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

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

export function handleSummary(data) {
  var output_file = __ENV.K6_TEST_RESULTS_HTML_FULL_FILE_PATH
  if(output_file) {
    console.log("generated HTML output file at path: ", output_file)
    return {
      [output_file]: htmlReport(data)
    };
  }
}
