function GenerateBalanceFactors = GenerateBalanceFactors(numberOfParticipants)

for i = 1:numberOfParticipants
    SOALevels = ["SOA1", "SOA2", "SOA3", "SOA4"];
    Interactions = ["Pour", "Place"];
    Placements = ["Left", "Right"];
    nConditions = length(SOALevels) * length(Interactions) * length(Placements);
    experimentalTrials = nConditions*10;
    randomTrials = 1;
    trialsPerCondition = ceil(experimentalTrials/nConditions);
    [SOAOrdered,InteractionsOrdered, PlacementsOrdered] = BalanceFactors(trialsPerCondition, randomTrials, SOALevels, Interactions, Placements);
    
    resultName = 'BalancedFactors';
    if i < 10
        tostr = int2str(i);
        iDToString = strcat('0',tostr); 
    else
        iDToString = int2str(i);
    end
    
    resultFileName=[iDToString '_' resultName '.txt'];
    resultFileHeader = 'SOA\t Interactions\t Placements\t Progress\n';
    resultFileFormatSpecifier = '%s\t %s\t %s\t %s\n';
    resultFile = fopen(resultFileName, 'w');
    fprintf(resultFile, resultFileHeader);
    progress = zeros(160,1);
    balancedFactors = [SOAOrdered InteractionsOrdered PlacementsOrdered progress];
    fprintf(resultFile, resultFileFormatSpecifier, balancedFactors');
    fclose(resultFile);
end
end
