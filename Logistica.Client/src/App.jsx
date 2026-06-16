import { useState } from "react";
import { fetchReporte } from "./services/api";
import "./App.css";

function App() {
  const [fechaInicio, setFechaInicio] = useState("2025-05-01");
  const [fechaFin, setFechaFin] = useState("2025-05-31");
  const [reporte, setReporte] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [consultado, setConsultado] = useState(false);

  const handleFiltrar = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setConsultado(true);

    try {
      const data = await fetchReporte(fechaInicio, fechaFin);
      setReporte(data);
    } catch (err) {
      setError(err.message || "Error al conectar con el servidor");
      setReporte([]);
    } finally {
      setLoading(false);
    }
  };

  const formatCurrency = (value) =>
    `$${Number(value).toFixed(2)}`;

  const formatDecimal = (value) =>
    Number(value).toFixed(2);

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>Mini Core de Logística</h1>
        <p className="app-subtitle">
          Sistema de reportes — .NET 8 + React 19 + Supabase
        </p>
      </header>

      <main className="app-main">
        {/* ── Formulario de filtros ── */}
        <form className="filter-form" onSubmit={handleFiltrar}>
          <div className="filter-group">
            <label htmlFor="fechaInicio">Fecha Inicio</label>
            <input
              id="fechaInicio"
              type="date"
              value={fechaInicio}
              onChange={(e) => setFechaInicio(e.target.value)}
              required
            />
          </div>

          <div className="filter-group">
            <label htmlFor="fechaFin">Fecha Fin</label>
            <input
              id="fechaFin"
              type="date"
              value={fechaFin}
              onChange={(e) => setFechaFin(e.target.value)}
              required
            />
          </div>

          <button type="submit" className="btn-filter" disabled={loading}>
            {loading ? "Consultando…" : "Calcular"}
          </button>
        </form>

        {/* ── Estado de error ── */}
        {error && (
          <div className="message message-error">
            <span>⚠</span> {error}
          </div>
        )}

        {/* ── Estado de carga ── */}
        {loading && (
          <div className="message message-loading">
            <div className="spinner"></div>
            Consultando datos desde Supabase…
          </div>
        )}

        {/* ── Tabla de resultados ── */}
        {!loading && consultado && !error && (
          <>
            {reporte.length === 0 ? (
              <div className="message message-empty">
                No se encontraron envíos en este período.
              </div>
            ) : (
              <div className="table-wrapper">
                <table className="report-table">
                  <thead>
                    <tr>
                      <th>Repartidor</th>
                      <th>Envíos</th>
                      <th>Total Kg</th>
                      <th>Zona</th>
                      <th>Tarifa Aplicada</th>
                      <th>Costo Total</th>
                    </tr>
                  </thead>
                  <tbody>
                    {reporte.map((row, index) => (
                      <tr
                        key={index}
                        className={
                          row.cantidadEnvios === 0 ? "row-empty" : ""
                        }
                      >
                        <td>{row.nombreRepartidor}</td>
                        <td className="cell-number">{row.cantidadEnvios}</td>
                        <td className="cell-number">
                          {formatDecimal(row.totalKg)}
                        </td>
                        <td>{row.zona}</td>
                        <td className="cell-number">
                          {row.tarifaAplicada > 0
                            ? formatCurrency(row.tarifaAplicada)
                            : "—"}
                        </td>
                        <td className="cell-number cell-cost">
                          {formatCurrency(row.costoTotal)}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </>
        )}
      </main>

      <footer className="app-footer">
        <p>Josue Cevallos — Universidad de las Fuerzas Armadas ESPE · 2025</p>
      </footer>
    </div>
  );
}

export default App;
