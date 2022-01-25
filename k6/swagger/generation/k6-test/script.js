/*
 * VehicleManagement API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * OpenAPI spec version: v1
 *
 * NOTE: This class is auto generated by OpenAPI Generator.
 * https://github.com/OpenAPITools/openapi-generator
 *
 * OpenAPI generator version: 5.4.0-SNAPSHOT
 */


import http from "k6/http";
import { group, check, sleep } from "k6";

const BASE_URL = "/";
// Sleep duration between successive requests.
// You might want to edit the value of this variable or remove calls to the sleep function on the script.
const SLEEP_DURATION = 0.1;
// Global variables should be initialized.

export default function() {
    group("/api/Vehicles/{licenseNumber}", () => {
        let licenseNumber = 'TODO_EDIT_THE_LICENSENUMBER'; // specify value as there is no example value for this parameter in OpenAPI spec

        // Request No. 1
        {
            let url = BASE_URL + `/api/Vehicles/${licenseNumber}`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });
        }
    });

    group("/api/Vehicles", () => {

        // Request No. 1
        {
            let url = BASE_URL + `/api/Vehicles`;
            let request = http.get(url);

            check(request, {
                "Success": (r) => r.status === 200
            });

            sleep(SLEEP_DURATION);
        }

        // Request No. 2
        {
            let url = BASE_URL + `/api/Vehicles`;
            let params = {headers: {"Content-Type": "application/json-patch+json", "Accept": "application/json"}};
            let request = http.post(url, params);

            check(request, {
                "Success": (r) => r.status === 200
            });
        }
    });

}
