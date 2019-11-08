import fetch from 'isomorphic-unfetch';

export async function getAsync<T>(url: string, headers?: HeadersInit): Promise<T> {
    headers = headers || {};

    headers['Accept'] = 'application/json';

    const response = await fetch(
        url,
        {
            headers,
            method: 'GET'
        });

    return await response.json();
}

export async function postAsync<T>(url: string, body: any, headers?: HeadersInit): Promise<T> {
    headers = headers || {};

    headers['Accept'] = 'application/json';

    if (body !== undefined) {
        headers['Content-Type'] = 'application/json';
    }

    const response = await fetch(
        url,
        {
            body: body ? JSON.stringify(body) : undefined,
            headers,
            method: 'POST'
        });

    return await response.json();
}
