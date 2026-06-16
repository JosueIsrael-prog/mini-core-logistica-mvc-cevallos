const API_BASE_URL = "http://localhost:5122";

/**
 * Consulta el reporte logístico del backend filtrado por rango de fechas.
 * @param {string} fechaInicio - Fecha inicio en formato YYYY-MM-DD
 * @param {string} fechaFin - Fecha fin en formato YYYY-MM-DD
 * @returns {Promise<Array>} Lista de registros del reporte
 */
export async function fetchReporte(fechaInicio, fechaFin) {
  const params = new URLSearchParams({ fechaInicio, fechaFin });
  const response = await fetch(`${API_BASE_URL}/api/logistica/reporte?${params}`);

  if (!response.ok) {
    throw new Error(`Error ${response.status}: ${response.statusText}`);
  }

  return response.json();
}
