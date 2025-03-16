import React, { useEffect, useRef } from "react";
import * as d3 from "d3";
import { RoomTemplate } from "@/types/roomTemplate";

type DungeonVisualizerProps = {
  templates: RoomTemplate[];
};

const DungeonVisualizer: React.FC<DungeonVisualizerProps> = ({ templates }) => {
  const svgRef = useRef<SVGSVGElement | null>(null);

  useEffect(() => {
    if (!svgRef.current || templates.length === 0) return;

    const svg = d3.select(svgRef.current);
    const width = 800;
    const height = 600;

    // Clear previous visualization
    svg.selectAll("*").remove();

    // Set up the SVG canvas
    svg.attr("viewBox", `0 0 ${width} ${height}`).style("background", "#f9f9f9");

    // Define a color scale for room templates
    const colorScale = d3
      .scaleOrdinal<string>()
      .domain(templates.map((template) => template.name))
      .range(d3.schemeCategory10);

    // Define a simulation for layout
    const simulation = d3
      .forceSimulation(templates)
      .force(
        "center",
        d3.forceCenter(width / 2, height / 2)
      )
      .force(
        "collision",
        d3.forceCollide((d) => Math.max(d.size.width, d.size.height) / 2 + 10)
      )
      .force("x", d3.forceX(width / 2).strength(0.05))
      .force("y", d3.forceY(height / 2).strength(0.05))
      .on("tick", () => {
        svg
          .selectAll("rect")
          .data(templates)
          .join("rect")
          .attr("x", (d) => d.x! - d.size.width / 2)
          .attr("y", (d) => d.y! - d.size.height / 2)
          .attr("width", (d) => d.size.width)
          .attr("height", (d) => d.size.height)
          .attr("fill", (d) => colorScale(d.name))
          .attr("stroke", "#333")
          .attr("stroke-width", 2);

        svg
          .selectAll("text")
          .data(templates)
          .join("text")
          .attr("x", (d) => d.x!)
          .attr("y", (d) => d.y!)
          .attr("text-anchor", "middle")
          .attr("dominant-baseline", "middle")
          .attr("font-size", "12px")
          .attr("fill", "#000")
          .text((d) => d.name);
      });

    return () => {
      simulation.stop();
    };
  }, [templates]);

  return (
    <div className="dungeon-visualizer">
      <h2 className="text-xl font-bold mb-4">Dungeon Visualizer</h2>
      <svg ref={svgRef} className="w-full h-96"></svg>
    </div>
  );
};

export default DungeonVisualizer;
