import React, { useEffect, useState } from "react";
import { fetchData } from "../services/api";

const Dashboard = ({ token }) => {
    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const getData = async () => {
            if (!token) {
                setError("No token found. Please log in.");
                setLoading(false);
                return;
            }

            try {
                console.log("Fetching data with token:", token);
                const result = await fetchData();

                if (result) {
                    setData(result);
                    setError(null);
                } else {
                    throw new Error("Failed to fetch data.");
                }
            } catch (err) {
                console.error("Fetch error:", err);
                setError(err.message || "An error occurred while fetching data.");
            } finally {
                setLoading(false);
            }
        };

        getData();
    }, [token]);

    if (loading) return <p>Loading...</p>;
    if (error) return <p style={{ color: "red" }}>Error: {error}</p>;

    return (
        <div>
            <h2>Dashboard</h2>
            <pre>{JSON.stringify(data, null, 2)}</pre>
        </div>
    );
};

export default Dashboard;