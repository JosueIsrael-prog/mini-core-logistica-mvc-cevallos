export async function fetchReporte(fechaInicio, fechaFin) {
  const params = new URLSearchParams({ fechaInicio, fechaFin });
  const response = await fetch(`/api/logistica/reporte?${params}`);

  if (!response.ok) {
    throw new Error(`Error ${response.status}: ${response.statusText}`);
  }

  return response.json();
}
