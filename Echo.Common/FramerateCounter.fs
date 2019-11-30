namespace Echo.Common

    open System.Collections.Generic
    open System.Linq

    type FramerateCounter() =
        let mutable totalFrames = 0
        let mutable totalSeconds = 0.0f
        let mutable averageFramesPerSecond = 0.0f
        let mutable currentFramesPerSecond = 0.0f

        let MAXIMUM_SAMPLES = 10

        let _sampleBuffer = new Queue<float32>();

        member this.AverageFramesPerSecond 
            with get() = averageFramesPerSecond

        member this.Update(deltaTime : float32) =
            currentFramesPerSecond <- 1.0f / deltaTime

            _sampleBuffer.Enqueue(currentFramesPerSecond)

            if _sampleBuffer.Count > MAXIMUM_SAMPLES then
                _sampleBuffer.Dequeue() |> ignore
                averageFramesPerSecond <- _sampleBuffer.Average(fun i -> i)
            else
                averageFramesPerSecond <- currentFramesPerSecond

            totalFrames <- totalFrames + 1;
            totalSeconds <- totalSeconds + deltaTime;
