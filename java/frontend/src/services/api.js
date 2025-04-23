const API_BASE_URL = "http://localhost:8083";  // Change this URL to your actual backend URL

export const loginUser = async (email, password) => {
    try {
        const response = await fetch(`${API_BASE_URL}/api/auth/login`, {  // Corrected string interpolation syntax
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',  // Indicating JSON body content
            },
            body: JSON.stringify({ email, password }),  // Send the email and password in the request body
        });

        // Check if the response is not ok (status code not in the range 200-299)
        if (!response.ok) {
            const errorText = await response.text();  // Get error message from response
            throw new Error(`Login failed: ${errorText || response.statusText}`);  // Handle response error message
        }

        // Assuming the response is a JSON object
        const data = await response.json();

        // If there's a valid response with companyInfo and token, return the data
        if (data && data.companyInfo && data.token) {
            return data;  // Return the entire response object containing companyInfo and token
        } else {
            throw new Error('Invalid credentials or empty response from server.');
        }
    } catch (error) {
        console.error("Error during login:", error);  // Log error to the console
        throw new Error(error.message || 'Login failed. Please try again.');  // Return custom error message
    }
};
